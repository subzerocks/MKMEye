/*
	MKMEye

	MKMEye developed by Alexander Pick - Copyright 2017
	Based on Magic Vision Created by Peter Simard - Copyright 2010

	This file is part of MKMEye
 
	MKMEye is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    MKMEye is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with MKMEye.  If not, see <http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von MKMEye.

    MKMEye ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Wahl) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.
    Fubar wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHRLEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.
    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ImageDBBuilder
{
    public class MySqlClient
    {
        private readonly MySqlConnection sql;

        public MySqlClient(string SqlConString)
        {
            sql = new MySqlConnection(SqlConString);
            sql.Open();
        }

        public DataRow dbRow(string query)
        {
            var command = sql.CreateCommand();
            command.CommandText = query;

            var selectDT = new DataTable();
            var dataAd = new MySqlDataAdapter(command);

            dataAd.Fill(selectDT);

            if (selectDT.Rows.Count > 0)
                return selectDT.Rows[0];
            return null;
        }

        public int lastInsertId()
        {
            var r = dbRow("SELECT last_insert_id() as lid");

            var id = (long) r[0];

            return (int) id;
        }

        public int affectedRows()
        {
            var r = dbRow("SELECT ROW_COUNT()");
            var id = (int) r[0];

            return id;
        }

        public DataTable dbResult(string query)
        {
            var command = sql.CreateCommand();
            command.CommandText = query;

            var selectDT = new DataTable();
            var dataAd = new MySqlDataAdapter(command);

            dataAd.Fill(selectDT);

            return selectDT;
        }

        internal int dbNone(string query)
        {
            var command = sql.CreateCommand();
            //MySqlDataReader Reader;
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        public DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}