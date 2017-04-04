using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MagicVision
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