using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace ImageDBBuilder
{
    public partial class MainView : Form
    {
        public MySqlClient sql;

        public MainView()
        {
            InitializeComponent();

            try
            {
                var xConfigFile = new XmlDocument();

                xConfigFile.Load(@".\\config.xml");

                var SqlConString = "server=" + xConfigFile.SelectSingleNode("/config/mysql/host").InnerText + ";" +
                                   "port=" + xConfigFile.SelectSingleNode("/config/mysql/port").InnerText + ";" +
                                   "database=" + xConfigFile.SelectSingleNode("/config/mysql/database").InnerText +
                                   ";" +
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
                    File.Delete(cardsPath);

                ZipFile.ExtractToDirectory(zipPath, @".\\");

                var jsonText = File.ReadAllText(cardsPath);

                var jsonData = JObject.Parse(jsonText);

                Console.WriteLine(jsonData.SelectTokens("$.*").Count());

                foreach (var edition in jsonData.SelectTokens("$.*"))
                {
                    var jcards = JArray.Parse(edition.SelectToken("$.cards").ToString());

                    foreach (var jcard in jcards)
                        try
                        {
                            logBox.AppendText("Processing " + jcard["multiverseid"] + "\n");

                            //pHash

                            ulong pHash = 0;

                            //var charsToRemove = new string[] { "@", ":", ";" };

                            var sFilteredFilename = jcard["imageName"].ToString();

                            /*foreach (var c in charsToRemove)
                            {
                                sFilteredFilename = sFilteredFilename.Replace(c, string.Empty);
                            }*/

                            var imageLocalJPG = pathBox.Text + "\\" + edition["code"] + "\\" + sFilteredFilename +
                                                postfixBox.Text + ".jpg";

                            if (!File.Exists(imageLocalJPG))
                            {
                                //MessageBox.Show("File not found! " + imageLocalJPG);
                            }

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
                                sql.dbNone(sSQLString);
                        }
                        catch (Exception e)
                        {
                            // just catch
                            //MessageBox.Show(e.Message);
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

        public class Phash
        {
            [DllImport(".\\phash\\phash.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ph_dct_imagehash(string file_name, ref ulong Hash);
        }
    }
}