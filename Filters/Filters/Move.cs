using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    class Move : Filters
    {
        int hor = 0;
        int vert = 0;
        public Move(int h, int v)
        {
            this.hor = h;
            this.vert = v;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {//cherniy cvet tam gde ne stalo izobrajeniya
            if ((x + hor * (sourceImage.Width / 100)) < sourceImage.Width & (y + vert * (sourceImage.Height / 100)) < sourceImage.Height)
                return sourceImage.GetPixel(Clamp(x + hor * (sourceImage.Width / 100), 0, sourceImage.Width - 1), Clamp(y + vert * (sourceImage.Height / 100), 0, sourceImage.Height - 1));
            else
                return Color.FromArgb(0, 0, 0);
        }
    }
}
