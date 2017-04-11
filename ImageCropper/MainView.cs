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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
        private Bitmap cameraBitmap;
        private Bitmap cameraBitmapLive;
        private Bitmap cardBitmap;
        private int currentIndex = 1;
        private Bitmap filteredBitmap;

        public MainView()
        {
            InitializeComponent();
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

        // Conver list of AForge.NET's points to array of .NET points
        private Point[] ToPointsArray(List<IntPoint> points)
        {
            var array = new Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
                array[i] = new Point(points[i].X, points[i].Y);

            return array;
        }

        private Bitmap detectQuads(Bitmap bitmap)
        {
            var cardArtBitmap = bitmap;

            try
            {
                /* 
                 *    This code works and crops well on 80% of the images but we need 100%
                 *  */

                var width = bitmap.Width + bitmap.Width / 2;
                var height = bitmap.Height + bitmap.Height / 2;

                // Create a compatible bitmap
                var dest = new Bitmap(width, height, bitmap.PixelFormat);
                var gd = Graphics.FromImage(dest);

                dest.Dispose();

                gd.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

                // Greyscale
                filteredBitmap = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);

                // Contrast
                //ContrastStretch filter = new ContrastStretch();
                //filter.ApplyInPlace(filteredBitmap);

                var cutMargin = 80;

                // split the image to avoid the textbox beeing detected
                var cropFilterPre =
                    new Crop(new Rectangle(0, cutMargin, filteredBitmap.Width, filteredBitmap.Height / 100 * 60));
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
                blobCounter.MinHeight = 200;
                blobCounter.MinWidth = 200;

                blobCounter.ProcessImage(bitmapData);
                var blobs = blobCounter.GetObjectsInformation();
                filteredBitmap.UnlockBits(bitmapData);

                var shapeChecker = new SimpleShapeChecker();

                /*var bm = new Bitmap(filteredBitmap.Width, filteredBitmap.Height, PixelFormat.Format24bppRgb);

                var g = Graphics.FromImage(bm);
                g.DrawImage(filteredBitmap, 0, 0);

                var pen = new Pen(Color.Red, 5);
                //var cardPositions = new List<IntPoint>();*/

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
                            // Hack to prevent it from detecting smaller sections of the card instead of the art
                            if (GetArea(corners) < 120000)
                                continue;

                            //debug crap
                            /*Bitmap newImage = filteredBitmap;
                            Random rnd = new Random();
                            newImage.Save(@"X:\" + rnd.Next() + ".jpg", ImageFormat.Jpeg);*/

                            var x = corners[0].X;
                            var y = corners[0].Y + cutMargin;
                            var x2 = corners[2].X - corners[0].X;
                            var y2 = corners[2].Y - (corners[0].Y + 0);

                            if (x2 < 0 || y2 < 0)
                            {
                                x = corners[1].X;
                                y = corners[1].Y + cutMargin;
                                x2 = corners[3].X - corners[1].X;
                                y2 = corners[3].Y - (corners[1].Y + 0);
                            }

                            var cropframe = new Rectangle(x, y, x2, y2);

                            var gbfilter = new GaussianBlur(1, 5);
                            gbfilter.ApplyInPlace(bitmap);

                            var cropFilter = new Crop(cropframe);
                            cardArtBitmap = cropFilter.Apply(bitmap);

                            bitmap.Dispose();

                            // pen.Dispose();
                            // g.Dispose();

                            gd.Dispose();

                            filteredBitmap.Dispose();

                            return cardArtBitmap;
                        }
                    }
                }

                Console.WriteLine("Fallback Triggered!");

                var gfilter = new GaussianBlur(1, 5);
                gfilter.ApplyInPlace(bitmap);

                //Fallback default crop, assumes XLHQ CCGHQ images

                var cropFilterFallback = new Crop(new Rectangle(88, 121, 564, 440));
                cardArtBitmap = cropFilterFallback.Apply(bitmap);

                bitmap.Dispose();

                //pen.Dispose();
                //g.Dispose();

                gd.Dispose();

                filteredBitmap.Dispose();
            }
            catch (Exception e)
            {
                throw;
            }

            return cardArtBitmap;
        }


        private void DirSearch(string sDir)
        {
            foreach (var d in Directory.GetDirectories(sDir))
            {
                foreach (var f in Directory.GetFiles(d))
                {
                    Console.WriteLine(f);
                    logBox.AppendText(f + "\n");
                    cropImage(f);
                    GC.Collect();
                }
                DirSearch(d);
            }
        }

        private void cropImage(string f)
        {
            try
            {
                var sFilename = Path.GetFileNameWithoutExtension(f);

                var sTarget = Path.Combine(targetText.Text, Path.GetDirectoryName(f).Split('\\').LastOrDefault());

                if (!Directory.Exists(sTarget))
                    Directory.CreateDirectory(sTarget);

                var sTargetFileNamePath = Path.Combine(sTarget, sFilename + ".jpg");

                var image = new Bitmap(f);

                var newImage = detectQuads(image);

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
            var files = Directory.GetFiles(target_dir);
            var dirs = Directory.GetDirectories(target_dir);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs)
                DeleteDirectory(dir);

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