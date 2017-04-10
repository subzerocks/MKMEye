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

/* Original Magic Vision - Created by Peter Simard
 * Further developed by Alexander Pick
 * 
 * You are free to use this source code any way you wish, all I ask for is an attribution (Peter Simard)
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly Filters cameraFilters = new Filters();
        private readonly List<MagicCard> magicCards = new List<MagicCard>();
        public readonly List<ReferenceCard> referenceCards = new List<ReferenceCard>();
        private Bitmap cameraBitmap;
        private Bitmap cameraBitmapLive;
        private Capture capture;
        private Bitmap cardArtBitmap;
        private Bitmap cardBitmap;
        private int currentIndex = 1;
        private Bitmap filteredBitmap;
        private List<MagicCard> magicCardsLastFrame = new List<MagicCard>();
        public MySqlClient sql;
        public double fScaleFactor;

        private string currentMatch = "";

        private XmlDocument xResult = new XmlDocument();

        public MainView()
        {
            InitializeComponent();

            this.KeyPreview = true;

            if (!File.Exists(@".\\config.xml"))
            {
                MessageBox.Show("Config File missing! Please read the manual.");

                Application.Exit();
            }

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

                foreach (var Lang in MKM.dLanguages)
                {
                    try
                    {
                        var item = new MKM.ComboboxItem();

                        item.Text = Lang.Value;
                        item.Value = Lang.Key;

                        langCombo.Items.Add(item);

                        langCombo.SelectedIndex = 0;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

                conditionCombo.SelectedIndex = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                Application.Exit();
            }
        }

        private double GetDeterminant(double x1, double y1, double x2, double y2)
        {
            return x1*y2 - x2*y1;
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
            return area/2;
        }

        private void detectQuads(Bitmap bitmap)
        {
            // Greyscale
            filteredBitmap = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);

            // Contrast - try to sharpen edges
            //ContrastStretch filter = new ContrastStretch();
            //filter.ApplyInPlace(filteredBitmap);

            // edge filter 
            // This filters accepts 8 bpp grayscale images for processing

            //Alternatives:
            //DifferenceEdgeDetector edgeFilter = new DifferenceEdgeDetector();
            //HomogenityEdgeDetector filter = new HomogenityEdgeDetector();
            //CannyEdgeDetector filter = new CannyEdgeDetector( );

            var edgeFilter = new SobelEdgeDetector();
            edgeFilter.ApplyInPlace(filteredBitmap);

            // Threshhold filter
            var threshholdFilter = new Threshold(180);
            threshholdFilter.ApplyInPlace(filteredBitmap);

            var bitmapData = filteredBitmap.LockBits(
                new Rectangle(0, 0, filteredBitmap.Width, filteredBitmap.Height),
                ImageLockMode.ReadWrite, filteredBitmap.PixelFormat);

            var blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;

            //possible finetuning

            blobCounter.MinHeight = Convert.ToInt32(Int32.Parse(blobHigh.Text) * fScaleFactor);
            blobCounter.MinWidth = Convert.ToInt32(Int32.Parse(blobWidth.Text) * fScaleFactor);

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
                            if (corners[0].DistanceTo(point) < Convert.ToInt32(40 * fScaleFactor))
                                sameCard = true;
                        }

                        if (sameCard)
                            continue;
                        

                        // Hack to prevent it from detecting smaller sections of the card instead of the whole card
                        if (GetArea(corners) < Convert.ToInt32(Double.Parse(treasholdBox.Text) * fScaleFactor))
                        {
                            continue;
                        }

                        cardPositions.Add(corners[0]);

                        g.DrawPolygon(pen, ToPointsArray(corners));

                        // Extract the card bitmap
                        var transformFilter = new QuadrilateralTransformation(corners, Convert.ToInt32(211 *fScaleFactor), Convert.ToInt32(298 *fScaleFactor));
                        cardBitmap = transformFilter.Apply(cameraBitmap);

                        //extract Art
                        var artCorners = new List<IntPoint>();
                        artCorners.Add(new IntPoint(Convert.ToInt32(14 * fScaleFactor), Convert.ToInt32(35 * fScaleFactor)));
                        artCorners.Add(new IntPoint(Convert.ToInt32(193 * fScaleFactor), Convert.ToInt32(35 * fScaleFactor)));
                        artCorners.Add(new IntPoint(Convert.ToInt32(193 * fScaleFactor), Convert.ToInt32(168 * fScaleFactor)));
                        artCorners.Add(new IntPoint(Convert.ToInt32(14 * fScaleFactor), Convert.ToInt32(168 * fScaleFactor)));

                        // Extract the art bitmap
                        var cartArtFilter = new QuadrilateralTransformation(artCorners, Convert.ToInt32(183 *fScaleFactor), Convert.ToInt32(133 *fScaleFactor));
                        cardArtBitmap = cartArtFilter.Apply(cardBitmap);

                        var card = new MagicCard();
                        card.corners = corners;
                        card.cardBitmap = cardBitmap;
                        card.cardArtBitmap = cardArtBitmap;

                        magicCards.Add(card);

                        pen.Dispose();
                        g.Dispose();

                        filteredBitmap = bm;

                        return;
                    }
                }
            }

            pen.Dispose();
            g.Dispose();

            filteredBitmap = bm;
        }

        // Move the corners a fixed amount
 /*       private void shiftCorners(List<IntPoint> corners, IntPoint point)
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
        }*/


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

            targetPic.Parent = camWindow;
            targetPic.BackColor = Color.Transparent;
            targetPic.Location = new Point(0, 30);

            cameraBitmap = new Bitmap(800, 600);
            capture = new Capture(cameraFilters.VideoInputDevices[cameraFilters.VideoInputDevices.Count - 1],
                cameraFilters.AudioInputDevices[0]);
            
            Size maxSize = capture.VideoCaps.MaxFrameSize;

            if (maxSize.Height > 480)
            {
                capture.FrameSize = new Size(800, 600);
            }
            else
            {
                capture.FrameSize = new Size(640, 480);
            }

            fScaleFactor = 1; //Convert.ToDouble(maxSize.Height) / 480;

            logBox.AppendText("camera at " + maxSize.Width + "/" + maxSize.Height + " (Factor " + fScaleFactor + ")\n");

            //capture.FrameSize = maxSize;
            capture.PreviewWindow = cam;
            capture.FrameEvent2 += CaptureDone;
            capture.GrapImg();

            loadSourceCards();
        }

        private void loadSourceCards()
        {
            using (var Reader = sql.dbResult("SELECT * FROM cards_full WHERE pHash != '0'"))
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
                cameraBitmapLive = (Bitmap) cameraBitmap.Clone();
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

                //ContrastCorrection filter = new ContrastCorrection(15);
                //filter.ApplyInPlace(card.cardArtBitmap);

                // Write the image to disk to be read by the pHash library.. should really find
                // a way to pass a pointer to image data directly
                card.cardBitmap.Save("tempCard" + cardTempId + ".jpg", ImageFormat.Jpeg);


                // Calculate art bitmap hash
                ulong cardHash = 0;
               // Phash.ph_dct_imagehash("tempCard" + cardTempId + ".jpg", ref cardHash);
                Phash.ph_dct_imagehash(".\\tempCard" + cardTempId + ".jpg", ref cardHash);

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
                    //Console.WriteLine("Highest Similarity: " + bestMatch.name + " ID: " + bestMatch.cardId);

                    currentMatch = bestMatch.name;

                    var g = Graphics.FromImage(cameraBitmap);
                    g.DrawString(bestMatch.name, new Font("Tahoma", 25), Brushes.Black,
                        new PointF(card.corners[0].X - 29, card.corners[0].Y - 39));
                    g.DrawString(bestMatch.name, new Font("Tahoma", 25), Brushes.Red,
                        new PointF(card.corners[0].X - 30, card.corners[0].Y - 40));
                    g.Dispose();
                }
                else
                {
                    //Console.WriteLine("No Best Match found!\n");
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

        //prevent multiple events
        bool keystroke = false;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            Console.WriteLine(keyData.ToString());

            if (keyData == Keys.Q)
            {
                CheckMKM();
                keystroke = false;
                return true;
            }

            if (keyData == Keys.W)
            {
                loadProductAtIndex();
                return true;
            }
            if (keyData == Keys.S && !keystroke)
            {
                addMKM();
                keystroke = true;
                return true;

            }

            keystroke = false;
            return base.ProcessCmdKey(ref msg, keyData);
            
        }

        private void checkMKMButton_Click(object sender, EventArgs e)
        {
            CheckMKM();
        }

        private void loadProductAtIndex()
        {
            if (xResult.ChildNodes.Count != 0)
            {
                // select next   
                var count = xResult.GetElementsByTagName("product").Count;

                if (currentIndex > count)
                {
                    currentIndex = 1;
                }

                var xProduct = xResult.SelectSingleNode("/response/product[" + currentIndex + "]");

                currentIndex++;

                nameLabel.Text = xProduct["enName"].InnerText;

                //avgLabel.Text = xProduct["enName"].InnerText;

                pidLabel.Text = xProduct["idProduct"].InnerText;

                editionLabel.Text = xProduct["expansionName"].InnerText;

                string imageURL = "https://www.magickartenmarkt.de/" + xProduct["image"].InnerText;

                Console.WriteLine(imageURL);

                detectedCard.ImageLocation = imageURL;

                XmlDocument xResultTmp =
                    MKM.makeRequest(
                        "https://www.mkmapi.eu/ws/v2.0/products/" + xProduct["idProduct"].InnerText,
                        "GET");

                if (xResultTmp.ChildNodes.Count != 0)
                {
                    xProduct = xResultTmp.SelectSingleNode("/response/product/priceGuide");

                    priceBox.Text = xProduct["AVG"].InnerText;

                }
            }

        }

        private void CheckMKM()
        {
            // https://www.mkmapi.eu/ws/v2.0/products/find?search=Springleaf&idGame=1&idLanguage=1

            currentIndex = 1;

            var sCardName = currentMatch;
            try
            {
                xResult =
                    MKM.makeRequest(
                        "https://www.mkmapi.eu/ws/v2.0/products/find?search=" + sCardName + "&idGame=1&idLanguage=1",
                        "GET");

                loadProductAtIndex();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void addMKM()
        {

                    logBox.AppendText(pidLabel.Text + " " + nameLabel.Text + " (" + 
                        (langCombo.SelectedItem as MKM.ComboboxItem).Value.ToString() + "\\" +
                        conditionCombo.Text + ")\n");

                    /*
                     3. Add an article to the user's stock:

                    POST https://www.mkmapi.eu/ws/v2.0/stock
                    */

                    var xBody = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                    "<request><article><idProduct>" + pidLabel.Text + "</idProduct><idLanguage>" +
                    (langCombo.SelectedItem as MKM.ComboboxItem).Value.ToString() +
                    "</idLanguage>" +
                    "<comments></comments><count>1</count><price>" + priceBox.Text + "</price><condition>"+
                    conditionCombo.Text +
                    "</condition>" +
                    "<isFoil>false</isFoil><isSigned>false</isSigned><isPlayset>false</isPlayset></article></request>";

                    MKM.makeRequest("https://www.mkmapi.eu/ws/v2.0/stock", "POST", xBody);

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
 