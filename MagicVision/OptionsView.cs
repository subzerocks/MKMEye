using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace MKMEye
{

    public partial class OptionsView : Form
    {
        private readonly MainView frm1;

        public OptionsView(MainView frm)
        {
            frm1 = frm;

            InitializeComponent();
        }

        private void downloadMultiverseImage(string sMultiverseId)
        {
            var imageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=" + sMultiverseId +
                           "&type=card";

            var imageLocal = Settings.refCardDir + sMultiverseId + ".png";

            using (var Client = new WebClient())
            {
                Client.DownloadFile(imageUrl, imageLocal);
            }

            ulong pHash = 0;
        
            Phash.ph_dct_imagehash(imageLocal, ref pHash);

            frm1.sql.dbNone("UPDATE cards SET pHash = '" + pHash + "' WHERE id = '" + sMultiverseId + "'");
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

                //JArray jsonData = JArray.Parse(jsonText);

                var jsonData = JObject.Parse(jsonText);

                Console.WriteLine(jsonData.SelectTokens("$.*").Count());

                foreach (var edition in jsonData.SelectTokens("$.*"))
                {
                    //Console.WriteLine(info.SelectToken("border").ToString());

                    var jcards = JArray.Parse(edition.SelectToken("$.cards").ToString());

                    foreach (var jcard in jcards)
                    {
                        try
                        {
                            logBox.AppendText("Downloading " + jcard["multiverseid"].ToString() + "\n");

                            string sSQLString =
                                "INSERT INTO cards (`id`,`Name`,`pHash`,`Set`,`Type`,`Cost`,`Rarity`) VALUES ('" +
                                jcard["multiverseid"].ToString() + "','" +
                                jcard["name"].ToString() + "','0','" +
                                edition.SelectToken("name").ToString() + "','" +
                                jcard["type"].ToString() + "','" +
                                jcard["manaCost"].ToString() + "','" +
                                jcard["rarity"].ToString() + "')";

                            //Console.WriteLine(sSQLString);

                            frm1.sql.dbNone(sSQLString);

                            downloadMultiverseImage(jcard["multiverseid"].ToString());
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
            frm1.sql.dbNone("DELETE FROM cards WHERE 1");
        }

        // http://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true
        // Thanks to Jeremy Edwards for this

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        private void checkRefCardDir()
        {
            if (Directory.Exists(Settings.refCardDir))
            {
                DeleteDirectory(Settings.refCardDir);
            }

            Directory.CreateDirectory(Settings.refCardDir);

        }

        private void updateDatabase_Click(object sender, EventArgs e)
        {
            purgeDB();
            checkRefCardDir();
            downloadJson();
        }
    }
}