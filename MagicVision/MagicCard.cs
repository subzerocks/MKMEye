using System.Collections.Generic;
using System.Drawing;
using AForge;

namespace MagicVision
{
    internal class MagicCard
    {
        public Bitmap cardArtBitmap;
        public Bitmap cardBitmap;
        public List<IntPoint> corners;
        public ReferenceCard referenceCard;
    }
}