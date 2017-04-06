using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using System.Xml;

namespace ImageDBBuilder
{
    public partial class MainView : Form
    {
        public MySqlClient sql;

        public class Phash
        {

            [DllImport(".\\pHash\\pHash.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ph_dct_imagehash(string file_name, ref ulong Hash);

        }

        public MainView()
        {
            InitializeComponent();

            try
            {
                var xConfigFile = new XmlDocument();

                xConfigFile.Load(@".\\config.xml");

                var SqlConString = "server=" + xConfigFile.SelectSingleNode("/config/mysql/host").InnerText + ";" +
                                   "port=" + xConfigFile.SelectSingleNode("/config/mysql/port").InnerText + ";" +
                                   "database=" + xConfigFile.SelectSingleNode("/config/mysql/database").InnerText + ";" +
                                   "uid=" + xConfigFile.SelectSingleNode("/config/mysql/username").InnerText + ";" +
                                   "pwd=" + xConfigFile.SelectSingleNode("/config/mysql/password").InnerText + ";" +
                                   "Allow Zero Datetime=true;";

                sql = new MySqlClient(SqlConString);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void downloadJson()
        {
            var zipPath = @".\\alljson.zip";

            using (var Client = new WebClient())
            {
                Client.DownloadFile("https://mtgjson.com/json/AllSets.json.zip", zipPath);
            }

            var cardsPath = @".\\AllSets.json";

            try
            {
                if (File.Exists(cardsPath))
                {
                    File.Delete(cardsPath);
                }

                ZipFile.ExtractToDirectory(zipPath, @".\\");

                var jsonText = File.ReadAllText(cardsPath);

                var jsonData = JObject.Parse(jsonText);

                Console.WriteLine(jsonData.SelectTokens("$.*").Count());

                foreach (var edition in jsonData.SelectTokens("$.*"))
                {

                    var jcards = JArray.Parse(edition.SelectToken("$.cards").ToString());

                    foreach (var jcard in jcards)
                    {
                        try
                        {
                            logBox.AppendText("Processing " + jcard["multiverseid"] + "\n");

                            //pHash

                            ulong pHash = 0;

                            var charsToRemove = new string[] { "@", ":", ";" };

                            string sFilteredFilename = jcard["name"].ToString();

                            foreach (var c in charsToRemove)
                            {
                                sFilteredFilename = sFilteredFilename.Replace(c, string.Empty);
                            }

                            var imageLocalJPG = pathBox.Text + "\\" + edition["code"] + "\\" + sFilteredFilename + ".jpg";

                            Phash.ph_dct_imagehash(imageLocalJPG, ref pHash);

                            var sSQLString =
                                "INSERT INTO cards (`id`,`Name`,`Edition`,`pHash`) VALUES ('" +
                                jcard["multiverseid"] + "','" +
                                sql.EncodeMySqlString(jcard["name"].ToString()) + "','" +
                                edition["code"] + "','"
                                + pHash +
                                "')";

                            logBox.AppendText(sSQLString + "\n");
                        
                            if (jcard["multiverseid"].ToString() != "")
                            {
                                sql.dbNone(sSQLString);
                            }

                        }
                        catch (Exception e)
                        {
                            // just catch
                            //MessageBox.Show(e.Message);
                        }
                    }
                }

                //Console.WriteLine(jsonData.ToString());

                //StreamWriter file = new StreamWriter(@".\\jsonformat.txt", true);

                //file.Write(jsonData.ToString());

                //file.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void purgeDB()
        {
            sql.dbNone("DELETE FROM cards WHERE 1");
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            purgeDB();
            downloadJson();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {

        }
    }
}