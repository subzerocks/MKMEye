using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using Point = System.Drawing.Point;

namespace ImageCropper
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }
        private Bitmap cameraBitmap;
        private Bitmap cameraBitmapLive;
        private Bitmap cardBitmap;
        private int currentIndex = 1;
        private Bitmap filteredBitmap;

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

        private Bitmap detectQuads(Bitmap bitmap)
        {


            Bitmap cardArtBitmap = bitmap;

            try
            {
             /* 
              *    This code works and crops well on 80% of the images but we need 100%
              *  
                // Greyscale
                filteredBitmap = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);

                // Contrast
                //ContrastStretch filter = new ContrastStretch();
                //filter.ApplyInPlace(filteredBitmap);

                // split the image to avoid the textbox beeing detected
                Crop cropFilterPre =
                    new Crop(new Rectangle(0, 0, filteredBitmap.Width, ((filteredBitmap.Height / 100) * 70)));
                filteredBitmap = cropFilterPre.Apply(filteredBitmap);

                // edge filter
                var edgeFilter = new SobelEdgeDetector(); //
                edgeFilter.ApplyInPlace(filteredBitmap);

                // Threshhold filter
                var threshholdFilter = new Threshold(190);
                threshholdFilter.ApplyInPlace(filteredBitmap);

                var bitmapData = filteredBitmap.LockBits(
                    new Rectangle(0, 0, filteredBitmap.Width, filteredBitmap.Height),
                    ImageLockMode.ReadWrite, filteredBitmap.PixelFormat);

                var blobCounter = new BlobCounter();

                blobCounter.FilterBlobs = true;
                blobCounter.MinHeight = 100;
                blobCounter.MinWidth = 120;

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

                            // Hack to prevent it from detecting smaller sections of the card instead of the whole card
                            if (GetArea(corners) < 100000)
                                continue;

                            Crop cropFilter =
                                new Crop(new Rectangle(corners[0].X, corners[0].Y, corners[2].X - corners[0].X,
                                    corners[2].Y - corners[0].Y));
                            cardArtBitmap = cropFilter.Apply(bitmap);

                            bitmap.Dispose();

                            pen.Dispose();
                            g.Dispose();

                            filteredBitmap.Dispose();

                            return cardArtBitmap;

                        }
                    }

                }*/

                //Fallback default crop, assumes XLHQ CCGHQ images

                Crop cropFilterFallback = new Crop(new Rectangle(88, 121, 564, 440));
                cardArtBitmap = cropFilterFallback.Apply(bitmap);

                bitmap.Dispose();

                //pen.Dispose();
                //g.Dispose();

                //filteredBitmap.Dispose();

            }
            catch (Exception e)
            {
                throw;
            }

            return cardArtBitmap;
        }



        private void DirSearch(string sDir)
        {

            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    logBox.AppendText(f + "\n");
                    cropImage(f);
                }
                DirSearch(d);
            }
        }

        private void cropImage(string f)
        {
            try
            {
                string sFilename = Path.GetFileNameWithoutExtension(f);

                string sTarget = Path.Combine(targetText.Text, Path.GetDirectoryName(f).Split('\\').LastOrDefault());

                if (!Directory.Exists(sTarget))
                {
                    Directory.CreateDirectory(sTarget);
                }

                string sTargetFileNamePath = Path.Combine(sTarget, sFilename.Split('.').First() + ".jpg");

                Bitmap image = new Bitmap(f);

                Bitmap newImage = detectQuads(image);

                image.Dispose();

                newImage.Save(sTargetFileNamePath, ImageFormat.Jpeg);
                newImage.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        // Thanks to Jeremy Edwards
        // http://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true

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

        private void cropButton_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists(sourceText.Text))
            {
                MessageBox.Show("Source Directory does not exist!");
                return;
            }

            try
            {
                DeleteDirectory(targetText.Text);
            }
            catch (Exception e2)
            {
                //nothing
            }

            Directory.CreateDirectory(targetText.Text);

            DirSearch(sourceText.Text);
        }
    }
}
