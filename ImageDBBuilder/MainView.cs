using System;
using System.Data.SQLite;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using MKMEye;
using Newtonsoft.Json.Linq;

namespace ImageDBBuilder
{
    public partial class MainView : Form
    {
        private readonly SQLiteClient sql;

        public MainView()
        {
            InitializeComponent();

            try
            {
                var cardsDBName = "cards_" + GetTimestamp(DateTime.Now) + ".sqlite";

                logBox.AppendText(cardsDBName + " created\n");

                sql = new SQLiteClient("Data Source=" + cardsDBName + ";Version=3;");

                var sCreate =
                    "CREATE TABLE cards(id integer NOT NULL, name varchar(255), pHash varchar(255), Edition varchar(8), PRIMARY KEY(id))";

                sql.dbNone(sCreate);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        private void processImages(MainView gui, SQLiteClient csql)
        {
            var efile = new StreamWriter(@".\\errorlog_" + GetTimestamp(DateTime.Now) + ".txt", true);

            BeginInvoke(new Action(() => { gui.logBox.AppendText("Downloading JSON Information\n"); }));

            var zipPath = @".\\alljson.zip";

            using (var Client = new WebClient())
            {
                Client.DownloadFile("https://mtgjson.com/json/AllSets.json.zip", zipPath);
            }

            var cardsPath = @".\\AllSets.json";

            BeginInvoke(new Action(() => { gui.logBox.AppendText("Extracting...\n"); }));

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
                            if (string.IsNullOrEmpty(jcard["multiverseid"].ToString()))
                            {
                                efile.Write(jcard["name"] + " has no multiverseid\n");
                                continue;
                            }
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
                                BeginInvoke(new Action(() => { gui.logBox.AppendText(imageLocalJPG + " missing\n"); }));

                                efile.Write(imageLocalJPG + " missing\n");
                                continue;
                            }

                            BeginInvoke(new Action(() =>
                            {
                                gui.logBox.AppendText("Processing " + jcard["multiverseid"] + "\n");
                            }));

                            Phash.ph_dct_imagehash(imageLocalJPG, ref pHash);

                            var insertSQL =
                                new SQLiteCommand(
                                    "INSERT INTO cards (id, Name, Edition, pHash) VALUES (@p1, @p2, @p3, @p4)",
                                    csql.sql);

                            insertSQL.Parameters.AddWithValue("@p1", jcard["multiverseid"].ToString());
                            insertSQL.Parameters.AddWithValue("@p2", jcard["name"].ToString());
                            insertSQL.Parameters.AddWithValue("@p3", edition["code"].ToString());
                            insertSQL.Parameters.AddWithValue("@p4", pHash.ToString());

                            try
                            {
                                insertSQL.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }
                        catch (Exception e)
                        {
                            // just catch
                            //MessageBox.Show(e.Message);
                            efile.Write(e.Message + "\n");
                        }
                }

                //Console.WriteLine(jsonData.ToString());

                //StreamWriter file = new StreamWriter(@".\\jsonformat.txt", true);

                //file.Write(jsonData.ToString());

                efile.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void purgeDB()
        {
            sql.dbNone("DELETE FROM cards");
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            buildButton.Enabled = false;

            purgeDB();

            Task.Run(() => processImages(this, sql));
        }

        public class Phash
        {
            [DllImport(".\\phash\\phash.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ph_dct_imagehash(string file_name, ref ulong Hash);
        }
    }
}