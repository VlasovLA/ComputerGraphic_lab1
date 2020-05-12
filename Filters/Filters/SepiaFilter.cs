using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    class SepiaFilter : Filters
    {
        int kf = 25;
        public SepiaFilter(int k)
        {
            this.kf = k*5;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int Intensity = (int)(0.2126 * sourceColor.R + 0.7152 * sourceColor.G + 0.0722 * sourceColor.B);
            
            Color resultColor = Color.FromArgb(Clamp(Intensity + 2*kf, 0, 255), Clamp(Intensity + (int)0.5*kf, 0, 255), Clamp(Intensity - kf, 0, 255));
            return resultColor;
        }
    }
}
