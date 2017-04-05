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
                            logBox.AppendText("Downloading " + jcard["multiverseid"] + "\n");

                            var sSQLString =
                                "INSERT INTO cards (`id`,`Name`,`pHash`,`Set`,`Type`,`Cost`,`Rarity`) VALUES ('" +
                                jcard["multiverseid"] + "','" +
                                jcard["name"] + "','0','" +
                                edition.SelectToken("name") + "','" +
                                jcard["type"] + "','" +
                                jcard["manaCost"] + "','" +
                                jcard["rarity"] + "')";

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
            var files = Directory.GetFiles(target_dir);
            var dirs = Directory.GetDirectories(target_dir);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs)
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