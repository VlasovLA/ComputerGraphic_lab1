using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    class Waves : Filters
    {
        int value;
        bool hor;
        public Waves(int v, bool h)
        {
            if (v != 0)
                this.value = v * 8;
            else
                this.value = 1;
            this.hor = h;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int orientation;
            if (hor)
                orientation = x;
            else
                orientation = y;

            int resultX = Clamp((int)(x + 20 * Math.Sin(Math.PI*orientation/value)), 0, sourceImage.Width - 1);
            int resultY = y;
                return sourceImage.GetPixel(resultX, resultY);
        }
    }
}
