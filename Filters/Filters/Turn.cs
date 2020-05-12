using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Filters
{
    class Turn : Filters
    {
        int x0;
        int y0;
        int fi;
        public Turn(int f)
        {
            fi = f;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            x0 = sourceImage.Width/2;
            y0 = sourceImage.Height/2; 
            int resultX = (int)((x - x0) * Math.Cos(fi) - (y - y0) * Math.Sin(fi) + x0);
            int resultY = (int)((x - x0) * Math.Sin(fi) + (y - y0) * Math.Cos(fi) + y0);
            if ((resultX < sourceImage.Width) && (resultX >= 0) && (resultY >= 0) && (resultY < sourceImage.Height))
                return sourceImage.GetPixel(resultX, resultY);
            else
                return Color.FromArgb(0, 0, 0);
        }
    }
}
