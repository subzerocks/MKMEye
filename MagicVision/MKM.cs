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

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace MKMEye
{
    internal class MKM
    {
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public static Dictionary<string, string> dLanguages = new Dictionary<string, string>
        {
            {
                "1",
                "English"
            }
            ,
            {
                "2",
                "French"
            }
            ,
            {
                "3",
                "German"
            }
            ,
            {
                "4",
                "Spanish"
            }
            ,
            {
                "5",
                "Italian"
            },
            {
                "6",
                "Simplified Chinese"
            }
            ,
            {
                "7",
                "Japanese"
            }
            ,
            {
                "8",
                "Portuguese"
            }
            ,
            {
                "9",
                "Russian"
            }
            ,
            {
                "10",
                "Korean"
            }
            ,
            {
                "11",
                "Traditional Chinese"
            }
        };

        public static XmlDocument makeRequest(string url, string method, string body = null)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = method;

            var header = new MKMAuth.OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            if (body != null)
            {
                request.ServicePoint.Expect100Continue = false;
                request.ContentLength = body.Length;
                request.ContentType = "text/xml";

                var writer = new StreamWriter(request.GetRequestStream());

                writer.Write(body);
                writer.Close();
            }

            var response = request.GetResponse() as HttpWebResponse;
            var doc = new XmlDocument();
            doc.Load(response.GetResponseStream());

            return doc;
        }
    }
}