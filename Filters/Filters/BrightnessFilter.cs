using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    class BrightnessFilter : Filters
    {
        int kf = 25;
        public BrightnessFilter(int k)
        {
            this.kf = (k-5)*15;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + kf, 0, 255), Clamp(sourceColor.G + kf, 0, 255), Clamp(sourceColor.B + kf, 0, 255));
            return resultColor;
        }
    }
}
