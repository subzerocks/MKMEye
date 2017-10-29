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
using System.Linq;
using System.Text;
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

        // detecting matrix, stores detected cards to avoid fail detection
        private Dictionary<string, int> bestMatches = new Dictionary<string, int>();

        private Bitmap cameraBitmap;
        private Bitmap cameraBitmapLive;
        private Capture capture;
        private Bitmap cardArtBitmap;
        private Bitmap cardBitmap;
        private int currentIndex = 1;

        private string currentMatch = "";
        private Bitmap filteredBitmap;
        public double fScaleFactor;

        private bool initalMKMGet;

        //prevent multiple events
        private bool keystroke;

        private List<MagicCard> magicCardsLastFrame = new List<MagicCard>();
        private readonly SQLiteClient ssSql;

        private readonly Timer timer1;

        private XmlDocument xResult = new XmlDocument();

        public int selectedCamIndex;

        public MainView()
        {

            InitializeComponent();

            KeyPreview = true;

            scanDataView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);

            scanDataView.ColumnCount = 6;

            scanDataView.Columns[0].HeaderText = "Name";
            scanDataView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            scanDataView.Columns[1].HeaderText = "Edition";
            scanDataView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            scanDataView.Columns[2].HeaderText = "Price";
            scanDataView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            scanDataView.Columns[3].HeaderText = "Language";
            scanDataView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            scanDataView.Columns[4].HeaderText = "Condition";
            scanDataView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            scanDataView.Columns[5].HeaderText = "MKM ID";
            scanDataView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            scanDataView.Columns[5].ReadOnly = true;

            var foilColumn = new DataGridViewCheckBoxColumn();

            foilColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            foilColumn.CellTemplate = new DataGridViewCheckBoxCell();
            foilColumn.TrueValue = true;
            foilColumn.FalseValue = false;
            foilColumn.HeaderText = "Foil";
            foilColumn.Name = "Foil";

            scanDataView.Columns.Add(foilColumn);

            var signedColumn = new DataGridViewCheckBoxColumn();

            signedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            signedColumn.CellTemplate = new DataGridViewCheckBoxCell();
            signedColumn.TrueValue = true;
            signedColumn.FalseValue = false;
            signedColumn.HeaderText = "Signed";
            signedColumn.Name = "Signed";

            scanDataView.Columns.Add(signedColumn);

            var playsetColumn = new DataGridViewCheckBoxColumn();

            playsetColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            playsetColumn.CellTemplate = new DataGridViewCheckBoxCell();
            playsetColumn.TrueValue = true;
            playsetColumn.FalseValue = false;
            playsetColumn.HeaderText = "Playset";
            playsetColumn.Name = "Playset";

            scanDataView.Columns.Add(playsetColumn);

            if (!File.Exists(@".\\config.xml"))
            {
                MessageBox.Show("Config File missing! Please read the manual.");

                Application.Exit();
            }

            try
            {
                ssSql = new SQLiteClient("Data Source=cards.sqlite;Version=3;");

                foreach (var Lang in MKM.dLanguages)
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

                conditionCombo.SelectedIndex = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                Application.Exit();
            }

            timer1 = new Timer();
            timer1.Tick += GarbageFire;
            timer1.Interval = 3000; // in miliseconds
            timer1.Start();
        }

        //flushs the detection cache - yes the name is from a card in conspiracy ;)
        private void GarbageFire(object sender, EventArgs e)
        {
            bestMatches = new Dictionary<string, int>();
        }


        private double GetDeterminant(double x1, double y1, double x2, double y2)
        {
            return x1 * y2 - x2 * y1;
        }

        private double GetArea(IList<IntPoint> vertices)
        {
            if (vertices.Count < 3)
                return 0;
            var area = GetDeterminant(vertices[vertices.Count - 1].X, vertices[vertices.Count - 1].Y, vertices[0].X,
                vertices[0].Y);
            for (var i = 1; i < vertices.Count; i++)
                area += GetDeterminant(vertices[i - 1].X, vertices[i - 1].Y, vertices[i].X, vertices[i].Y);
            return area / 2;
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
            var threshholdFilter = new Threshold(240); //180
            threshholdFilter.ApplyInPlace(filteredBitmap);

            var bitmapData = filteredBitmap.LockBits(
                new Rectangle(0, 0, filteredBitmap.Width, filteredBitmap.Height),
                ImageLockMode.ReadWrite, filteredBitmap.PixelFormat);

            var blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;

            //possible finetuning

            blobCounter.MinHeight = Convert.ToInt32(int.Parse(blobHigh.Text) * fScaleFactor); //fScaleFactor
            blobCounter.MinWidth = Convert.ToInt32(int.Parse(blobWidth.Text) * fScaleFactor); //fScaleFactor

#if DEBUG
            Console.WriteLine("Calculate min blobsize " + blobCounter.MinWidth + "/" + blobCounter.MinHeight);
#endif

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
                            if (corners[0].DistanceTo(point) < Convert.ToInt32(40 * fScaleFactor)) //fScaleFactor
                                sameCard = true;

                        if (sameCard)
                            continue;

                        /*
                         *  This code seems to have an issue if scaled up with the factor:
                         */

                        // Hack to prevent it from detecting smaller sections of the card instead of the whole card
                        if (GetArea(corners) < Convert.ToInt32(double.Parse(treasholdBox.Text) * fScaleFactor)
                        ) //fScaleFactor
                            continue;

                        cardPositions.Add(corners[0]);

                        g.DrawPolygon(pen, ToPointsArray(corners));

                        // Extract the card bitmap

                        // Debug
                        //var transformFilter = new QuadrilateralTransformation(corners, 600, 800);

                        var transformFilter = new QuadrilateralTransformation(corners,
                            Convert.ToInt32(211 * fScaleFactor), Convert.ToInt32(298 * fScaleFactor));

                        cardBitmap = transformFilter.Apply(cameraBitmap);

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
                if (pointDistances[x] < shortestDist)
                {
                    shortestSide = x;
                    shortestDist = pointDistances[x];
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
            /*targetPic.Parent = camWindow;
            targetPic.BackColor = Color.Transparent;
            targetPic.Location = new Point(0, 30);*/

            cameraBitmap = new Bitmap(800, 600);

            capture = new Capture(cameraFilters.VideoInputDevices[selectedCamIndex],
                cameraFilters.AudioInputDevices[0]);

            var maxSize = capture.VideoCaps.MaxFrameSize;

            capture.FrameSize = new Size(640, 480);

            if (maxSize.Height > 480)
                capture.FrameSize = new Size(800, 600);

            //fails my cam :(
            /*if (maxSize.Height >= 720)
                capture.FrameSize = new Size(960, 720);
            */

            if (maxSize.Height >= 768)
                capture.FrameSize = new Size(1024, 768);

            fScaleFactor = Convert.ToDouble(capture.FrameSize.Height) / 480;

            logBox.AppendText("camera max at " + maxSize.Width + "/" + maxSize.Height + "\n");
            logBox.AppendText("running at " + capture.FrameSize.Width + "/" + capture.FrameSize.Height + " (Factor " +
                              fScaleFactor + ")\n");

            //capture.FrameSize = maxSize;
            capture.PreviewWindow = cam;
            capture.FrameEvent2 += CaptureDone;
            capture.GrapImg();

            loadSourceCards();
        }

        private void loadSourceCards()
        {
            using (var Reader = ssSql.dbResult("SELECT * FROM cards WHERE pHash != '0'"))
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
                    var g = Graphics.FromImage(cameraBitmap);
                    g.DrawString(currentMatch, new Font("Tahoma", 25), Brushes.Black,
                        new PointF(card.corners[0].X - 29, card.corners[0].Y - 39));
                    g.DrawString(currentMatch, new Font("Tahoma", 25), Brushes.Red,
                        new PointF(card.corners[0].X - 30, card.corners[0].Y - 40));
                    g.Dispose();


#if DEBUG
                    Console.WriteLine("DEBUG: Highest Similarity: " + bestMatch.name + " ID: " + bestMatch.cardId);
#endif

                    if (bestMatches.ContainsKey(bestMatch.cardId))
                        bestMatches[bestMatch.cardId] += 1;
                    else
                        bestMatches[bestMatch.cardId] = 1;

                    //Console.WriteLine("DEBUG: Checking " + bestMatches[bestMatch.cardId]);


                    var maxValue = 0;
                    string bestMatchId = null;

                    foreach (var match in bestMatches)
                        if (match.Value > maxValue)
                        {
                            maxValue = match.Value;
                            bestMatchId = match.Key;
                        }

                    if (bestMatchId != bestMatch.cardId)
                        continue;
                }

                if (bestMatch != null)
                {
                    card.referenceCard = bestMatch;

                    currentMatch = bestMatch.name;


                    // highly experimental

#if NULL
                    const string tessDataDir = @".\\tessdata";

                    using (var engine = new TesseractEngine(tessDataDir, "eng", EngineMode.Default))
                    using (var image = Pix.LoadFromFile(".\\tempCard" + cardTempId + ".jpg"))
                    using (var page = engine.Process(image))
                    {
                        string text = page.GetText();
                        Console.WriteLine("DEBUG: Mean confidence: {0}", page.GetMeanConfidence());
                        Console.WriteLine("DEBUG: "+ text);
                    }
#endif
                }
            }
        }


        // Conver list of AForge.NET's points to array of .NET points
        private Point[] ToPointsArray(List<IntPoint> points)
        {
            var array = new Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
                array[i] = new Point(points[i].X, points[i].Y);

            return array;
        }

        private void addToList()
        {
            if (initalMKMGet == false)
            {
                CheckMKM();
                bestMatches = new Dictionary<string, int>();
            }

            scanDataView.Rows.Add(nameLabel.Text, editionLabel.Text, priceBox.Text, langCombo.Text, conditionCombo.Text,
                pidLabel.Text);

            logBox.AppendText(nameLabel.Text + " added to list\n");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData.ToString());

            if (keyData == Keys.Q)
            {
                CheckMKM();
                keystroke = false;
                return true;
            }

            if (keyData == Keys.L)
            {
                addToList();
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
            bestMatches = new Dictionary<string, int>();

            initalMKMGet = true;
        }

        private void loadProductAtIndex()
        {
            if (xResult.ChildNodes.Count != 0)
            {
                // select next   
                var count = xResult.GetElementsByTagName("product").Count;

                if (currentIndex > count)
                    currentIndex = 1;

                var xProduct = xResult.SelectSingleNode("/response/product[" + currentIndex + "]");

                currentIndex++;

                nameLabel.Text = xProduct["enName"].InnerText;

                //avgLabel.Text = xProduct["enName"].InnerText;

                pidLabel.Text = xProduct["idProduct"].InnerText;

                editionLabel.Text = xProduct["expansionName"].InnerText;

                var imageURL = "https://www.magickartenmarkt.de/" + xProduct["image"].InnerText;

                Console.WriteLine(imageURL);

                detectedCard.ImageLocation = imageURL;

                var xResultTmp =
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
                              (langCombo.SelectedItem as MKM.ComboboxItem).Value + "\\" +
                              conditionCombo.Text + ")\n");

            /*
             3. Add an article to the user's stock:

            POST https://www.mkmapi.eu/ws/v2.0/stock
            */

            var xBody = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                        "<request><article><idProduct>" + pidLabel.Text + "</idProduct><idLanguage>" +
                        (langCombo.SelectedItem as MKM.ComboboxItem).Value +
                        "</idLanguage>" +
                        "<comments></comments><count>1</count><price>" + priceBox.Text + "</price><condition>" +
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


        private void addToListButton_Click(object sender, EventArgs e)
        {
            addToList();
        }

        private void deleteFromListButton_Click(object sender, EventArgs e)
        {
            var row = scanDataView.CurrentCell.RowIndex;
            scanDataView.Rows.RemoveAt(row);
        }

        public static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        private void exportCSVButton_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var headers = scanDataView.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(";", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in scanDataView.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(";", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            }

            var sFilename = @".\\export_" + GetTimestamp(DateTime.Now) + ".csv";

            File.WriteAllText(sFilename, sb.ToString());

            MessageBox.Show("Exported to " + sFilename);
        }

        private void exportToMKMButton_Click(object sender, EventArgs e)
        {
            var xBody = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                        "<request>";

            /*"<article><idProduct>" + pidLabel.Text + "</idProduct><idLanguage>" +
                        (langCombo.SelectedItem as MKM.ComboboxItem).Value +
                        "</idLanguage>" +
                        "<comments></comments><count>1</count><price>" + priceBox.Text + "</price><condition>" +
                        conditionCombo.Text +
                        "</condition>" +
                        "<isFoil>false</isFoil><isSigned>false</isSigned><isPlayset>false</isPlayset></article > 
    */

            foreach (DataGridViewRow row in scanDataView.Rows)
                if (row.Cells[5].Value != "")
                    xBody += "<article><idProduct>" + row.Cells[5].Value + "</idProduct><idLanguage>" +
                             row.Cells[3].Value +
                             "</idLanguage>" +
                             "<comments></comments><count>1</count><price>" +
                             row.Cells[2].Value + "</price><condition>" +
                             row.Cells[4].Value +
                             "</condition>" +
                             "<isFoil>" +
                             Convert.ToString(row.Cells[6].Value) + "</isFoil><isSigned>" +
                             Convert.ToString(row.Cells[7].Value) + "</isSigned><isPlayset>" +
                             Convert.ToString(row.Cells[8].Value) + "</isPlayset></article >";

            xBody += "</request>";

            MKM.makeRequest("https://www.mkmapi.eu/ws/v2.0/stock", "POST", xBody);

            MessageBox.Show("Article were added to the marketplace!");
        }

        private void emptyListButton_Click(object sender, EventArgs e)
        {
            scanDataView.Rows.Clear();
        }
    }
}