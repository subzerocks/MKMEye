/*
	Magic Vision - MKM Edition 

	Magic Vision - MKM Edition developed by Alexander Pick - Copyright 2017
	Original Magic Vision Created by Peter Simard - Copyright 2010

	This file is part of MKMTool
 
	MagicVision MKM Edition is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    MagicVision MKM Edition is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with MagicVision MKM Edition.  If not, see <http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von MagicVision MKM Edition.

    MagicVision MKM Edition ist Freie Software: Sie können es unter den Bedingungen
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

/* Original Magic Vision - Created by Peter Simard
 * Further developed by Alexander Pick
 * 
 * You are free to use this source code any way you wish, all I ask for is an attribution (Peter Simard)
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using DirectX.Capture;
using Point = System.Drawing.Point;

namespace MKMEye
{

    public partial class MainView : Form
    {
        private static readonly object _locker = new object();
        private Bitmap cameraBitmap;
        private Bitmap cameraBitmapLive;
        private readonly Filters cameraFilters = new Filters();
        private Capture capture;
        private Bitmap cardArtBitmap;
        private Bitmap cardBitmap;
        private Bitmap filteredBitmap;
        private readonly List<MagicCard> magicCards = new List<MagicCard>();
        private List<MagicCard> magicCardsLastFrame = new List<MagicCard>();
        public readonly List<ReferenceCard> referenceCards = new List<ReferenceCard>();
        public MySqlClient sql;

        public MainView()
        {
            InitializeComponent();

            if (!File.Exists(@".\\config.xml"))
            {
                MessageBox.Show("Config File missing! Please read the manual.");

                Application.Exit();
            }

            try
            {

                XmlDocument xConfigFile = new XmlDocument();

                xConfigFile.Load(@".\\config.xml");

                string SqlConString = "server=" + xConfigFile.SelectSingleNode("/config/mysql/host").InnerText + ";" +
                                         "port=" + xConfigFile.SelectSingleNode("/config/mysql/port").InnerText + ";" +
                                         "database=" + xConfigFile.SelectSingleNode("/config/mysql/database").InnerText + ";" +
                                         "uid=" + xConfigFile.SelectSingleNode("/config/mysql/username").InnerText + ";" +
                                         "pwd=" + xConfigFile.SelectSingleNode("/config/mysql/password").InnerText + ";" +
                                         "Allow Zero Datetime=true;";

                sql = new MySqlClient(SqlConString);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());


                throw;
            }

        }

        private double GetDeterminant(double x1, double y1, double x2, double y2)
        {
            return x1 * y2 - x2 * y1;
        }

        private double GetArea(IList<IntPoint> vertices)
        {
            if (vertices.Count < 3)
            {
                return 0;
            }
            var area = GetDeterminant(vertices[vertices.Count - 1].X, vertices[vertices.Count - 1].Y, vertices[0].X,
                vertices[0].Y);
            for (var i = 1; i < vertices.Count; i++)
            {
                area += GetDeterminant(vertices[i - 1].X, vertices[i - 1].Y, vertices[i].X, vertices[i].Y);
            }
            return area / 2;
        }

        private void detectQuads(Bitmap bitmap)
        {
            // Greyscale
            filteredBitmap = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);

            // edge filter
            var edgeFilter = new SobelEdgeDetector();
            edgeFilter.ApplyInPlace(filteredBitmap);

            // Threshhold filter
            var threshholdFilter = new Threshold(190);
            threshholdFilter.ApplyInPlace(filteredBitmap);

            var bitmapData = filteredBitmap.LockBits(
                new Rectangle(0, 0, filteredBitmap.Width, filteredBitmap.Height),
                ImageLockMode.ReadWrite, filteredBitmap.PixelFormat);

            var blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = 125;
            blobCounter.MinWidth = 125;

            blobCounter.ProcessImage(bitmapData);
            var blobs = blobCounter.GetObjectsInformation();
            filteredBitmap.UnlockBits(bitmapData);

            var shapeChecker = new SimpleShapeChecker();

            var bm = new Bitmap(filteredBitmap.Width, filteredBitmap.Height, PixelFormat.Format24bppRgb);

            var g = Graphics.FromImage(bm);
            g.DrawImage(filteredBitmap, 0, 0);

            var pen = new Pen(Color.Red, 5);
            var cardPositions = new List<IntPoint>();


            // Loop through detected shapes
            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                var edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                List<IntPoint> corners;
                var sameCard = false;

                // is triangle or quadrilateral
                if (shapeChecker.IsConvexPolygon(edgePoints, out corners))
                {
                    // get sub-type
                    var subType = shapeChecker.CheckPolygonSubType(corners);

                    // Only return 4 corner rectanges
                    if ((subType == PolygonSubType.Parallelogram || subType == PolygonSubType.Rectangle) &&
                        corners.Count == 4)
                    {
                        // Check if its sideways, if so rearrange the corners so it's veritcal
                        rearrangeCorners(corners);

                        // Prevent it from detecting the same card twice
                        foreach (var point in cardPositions)
                        {
                            if (corners[0].DistanceTo(point) < 40)
                                sameCard = true;
                        }

                        if (sameCard)
                            continue;

                        // Hack to prevent it from detecting smaller sections of the card instead of the whole card
                        if (GetArea(corners) < 20000)
                            continue;

                        cardPositions.Add(corners[0]);

                        g.DrawPolygon(pen, ToPointsArray(corners));

                        // Extract the card bitmap
                        var transformFilter = new QuadrilateralTransformation(corners, 211, 298);
                        cardBitmap = transformFilter.Apply(cameraBitmap);

                        var artCorners = new List<IntPoint>();
                        artCorners.Add(new IntPoint(14, 35));
                        artCorners.Add(new IntPoint(193, 35));
                        artCorners.Add(new IntPoint(193, 168));
                        artCorners.Add(new IntPoint(14, 168));

                        // Extract the art bitmap
                        var cartArtFilter = new QuadrilateralTransformation(artCorners, 183, 133);
                        cardArtBitmap = cartArtFilter.Apply(cardBitmap);

                        var card = new MagicCard();
                        card.corners = corners;
                        card.cardBitmap = cardBitmap;
                        card.cardArtBitmap = cardArtBitmap;

                        magicCards.Add(card);
                    }
                }
            }

            pen.Dispose();
            g.Dispose();

            filteredBitmap = bm;
        }

        // Move the corners a fixed amount
        private void shiftCorners(List<IntPoint> corners, IntPoint point)
        {
            var xOffset = point.X - corners[0].X;
            var yOffset = point.Y - corners[0].Y;

            for (var x = 0; x < corners.Count; x++)
            {
                var point2 = corners[x];

                point2.X += xOffset;
                point2.Y += yOffset;

                corners[x] = point2;
            }
        }


        private void rearrangeCorners(List<IntPoint> corners)
        {
            var pointDistances = new float[4];

            for (var x = 0; x < corners.Count; x++)
            {
                var point = corners[x];

                pointDistances[x] = point.DistanceTo(x == corners.Count - 1 ? corners[0] : corners[x + 1]);
            }

            var shortestDist = float.MaxValue;
            var shortestSide = int.MaxValue;

            for (var x = 0; x < corners.Count; x++)
            {
                if (pointDistances[x] < shortestDist)
                {
                    shortestSide = x;
                    shortestDist = pointDistances[x];
                }
            }

            if (shortestSide != 0 && shortestSide != 2)
            {
                var endPoint = corners[0];
                corners.RemoveAt(0);
                corners.Add(endPoint);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cameraBitmap = new Bitmap(640, 480);
            capture = new Capture(cameraFilters.VideoInputDevices[cameraFilters.VideoInputDevices.Count - 1], cameraFilters.AudioInputDevices[0]);
            var vc = capture.VideoCaps;
            capture.FrameSize = new Size(640, 480);
            capture.PreviewWindow = cam;
            capture.FrameEvent2 += CaptureDone;
            capture.GrapImg();

            loadSourceCards();
        }

        private void loadSourceCards()
        {
            using (var Reader = sql.dbResult("SELECT * FROM cards"))
            {
                foreach (DataRow r in Reader.Rows)
                {
                    var card = new ReferenceCard();
                    card.cardId = r["id"].ToString();
                    card.name = r["Name"].ToString();
                    card.pHash = Convert.ToUInt64(r["pHash"]);
                    card.dataRow = r;

                    referenceCards.Add(card);
                }
            }
        }

        private void CaptureDone(Bitmap e)
        {

            //Console.WriteLine("CaptureDone() called");

            lock (_locker)
            {
                magicCardsLastFrame = new List<MagicCard>(magicCards);
                magicCards.Clear();
                cameraBitmap = e;
                cameraBitmapLive = (Bitmap)cameraBitmap.Clone();
                detectQuads(cameraBitmap);
                matchCard();

                image_output.Image = filteredBitmap;
                camWindow.Image = cameraBitmap;
            }
        }

        private void matchCard()
        {
            //Console.WriteLine("matchCard() called with " +  magicCards.Count + " cards detected");

            var cardTempId = 0;
            foreach (var card in magicCards)
            {
                cardTempId++;
                // Write the image to disk to be read by the pHash library.. should really find
                // a way to pass a pointer to image data directly
                card.cardArtBitmap.Save("tempCard" + cardTempId + ".jpg", ImageFormat.Jpeg);


                // Calculate art bitmap hash
                ulong cardHash = 0;
                Phash.ph_dct_imagehash("tempCard" + cardTempId + ".jpg", ref cardHash);

                var lowestHamming = int.MaxValue;
                ReferenceCard bestMatch = null;

                foreach (var referenceCard in referenceCards)
                {
                    var hamming = Phash.HammingDistance(referenceCard.pHash, cardHash);
                    if (hamming < lowestHamming)
                    {
                        lowestHamming = hamming;
                        bestMatch = referenceCard;
                    }
                }

                if (bestMatch != null)
                {
                    card.referenceCard = bestMatch;
                    Console.WriteLine("Highest Similarity: " + bestMatch.name + " ID: " + bestMatch.cardId.ToString());

                    var g = Graphics.FromImage(cameraBitmap);
                    g.DrawString(bestMatch.name, new Font("Tahoma", 25), Brushes.Black,
                        new PointF(card.corners[0].X - 29, card.corners[0].Y - 39));
                    g.DrawString(bestMatch.name, new Font("Tahoma", 25), Brushes.Yellow,
                        new PointF(card.corners[0].X - 30, card.corners[0].Y - 40));
                    g.Dispose();
                }
                else
                {
                    Console.WriteLine("No Best Match found!\n");
                }
            }
        }


        // Conver list of AForge.NET's points to array of .NET points
        private Point[] ToPointsArray(List<IntPoint> points)
        {
            var array = new Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new Point(points[i].X, points[i].Y);
            }

            return array;
        }

        private void camWindow_MouseClick(object sender, MouseEventArgs e)
        {
            lock (_locker)
            {
                foreach (var card in magicCards)
                {
                    var rect = new Rectangle(card.corners[0].X, card.corners[0].Y, card.corners[1].X - card.corners[0].X,
                        card.corners[2].Y - card.corners[1].Y);
                    if (rect.Contains(e.Location))
                    {
                        Debug.WriteLine(card.referenceCard.name);
                        /*cardArtImage.Image = card.cardArtBitmap;
                        cardImage.Image = card.cardBitmap;

                        cardInfo.Text = "Card Name: " + card.referenceCard.name + Environment.NewLine +
                                        "Set: " + (string)card.referenceCard.dataRow["Set"] + Environment.NewLine +
                                        "Type: " + (string)card.referenceCard.dataRow["Type"] + Environment.NewLine +
                                        "Casting Cost: " + (string)card.referenceCard.dataRow["Cost"] +
                                        Environment.NewLine +
                                        "Rarity: " + (string)card.referenceCard.dataRow["Rarity"] + Environment.NewLine;*/
                    }
                }
            }
        }


        private void optionsButton_Click(object sender, EventArgs e)
        {
            var Options = new OptionsView(this);
            Options.ShowDialog();
        }

        private void MainView_keydDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                CheckMKM();
            }

            if (e.KeyCode == Keys.W)
            {
                //add card
            }

        }

        private void checkMKMButton_Click(object sender, EventArgs e)
        {
            CheckMKM();
        }

        private XmlDocument xResult = new XmlDocument();
        private int currentIndex = 1;

        private void loadProductAtIndex()
        {
            if (xResult.ChildNodes.Count != 0)
            {
                // select next   
                XmlNode xProduct = xResult.SelectSingleNode("/product[" + currentIndex + "]");

                currentIndex++;

                nameLabel.Text = xProduct["enName"].InnerText;

                //avgLabel.Text = xProduct["enName"].InnerText;

                pidLabel.Text = xProduct["idProduct"].InnerText;

                editionLabel.Text = xProduct["expansionName"].InnerText;

                detectedCard.ImageLocation = "https://www.magickartenmarkt.de/" + xProduct["website"].InnerText;
            }
        }

        private void CheckMKM()
        {
            // https://www.mkmapi.eu/ws/v2.0/products/find?search=Springleaf&idGame=1&idLanguage=1

            string sCardName = "Springleaf";
            try
            {
                xResult = MKM.makeRequest("https://www.mkmapi.eu/ws/v2.0/products/find?search=" + sCardName + "&idGame=1&idLanguage=1", "GET");

                loadProductAtIndex();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void addMKM()
        {
            lock (_locker)
            {
                foreach (var card in magicCards)
                {
                    logBox.AppendText(card.referenceCard.name + " added to List\n");

                    /*
                     3. Add an article to the user's stock:

                    POST https://www.mkmapi.eu/ws/v2.0/stock

                    <?xml version="1.0" encoding="UTF-8" ?>
                    <request>
                        <article>
                            <idProduct>100569</idProduct>
                            <idLanguage>1</idLanguage>
                            <comments>Inserted through the API</comments>
                            <count>1</count>
                            <price>4</price>
                            <condition>EX</condition>
                            <isFoil>true</isFoil>
                            <isSigned>false</isSigned>
                            <isPlayset>false</isPlayset>
                        </article>
                    </request>

                    */

                    string xBody = "";

                    XmlDocument xResult = MKM.makeRequest("https://www.mkmapi.eu/ws/v2.0/products/find?search=", "POST", xBody);

                }
            }
        }

        private void addMKMButton_Click(object sender, EventArgs e)
        {

            addMKM();

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            loadProductAtIndex();
        }
    }
}
