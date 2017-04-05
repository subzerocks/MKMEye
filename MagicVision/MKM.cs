using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace MKMEye
{
    class MKM
    {
        public static Dictionary<string, string> dLanguages = new Dictionary<string, string>
        {
            {
                "",
                "All"
            }
            ,
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
