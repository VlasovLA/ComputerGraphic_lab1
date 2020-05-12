using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    class MaximumFilter : Filters
    {
        public MaximumFilter()
        {
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int br = 0;
            Color col = Color.Black;
            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    int curBr = (int)(neighborColor.R * 0.3 + neighborColor.G * 0.59 + neighborColor.B * 0.11);
                    if (br < curBr)
                    {
                        br = (int)(neighborColor.R * 0.3 + neighborColor.G * 0.59 + neighborColor.B * 0.11);
                        col = neighborColor;
                    }
                }
            }
            return col;
        }
    }
}
