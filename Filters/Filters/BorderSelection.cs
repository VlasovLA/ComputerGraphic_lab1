using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    class BorderSelection : MatrixFilters
    {
        int value;
        public BorderSelection(int val, bool hor)
        {
            this.value = val;
            kernel = new float[3, 3];
            if (hor)
            {
                kernel[0, 0] = value / 2; kernel[1, 0] = value * 2; kernel[2, 0] = -value / 2;
                kernel[0, 1] = value * 2; kernel[1, 1] = 0; kernel[2, 1] = -value * 2;
                kernel[0, 2] = value / 2; kernel[1, 2] = -value * 2; kernel[2, 2] = -value / 2;
            }
            else
            {
                kernel[0, 0] = value / 2; kernel[1, 0] = value * 2; kernel[2, 0] = value / 2;
                kernel[0, 1] = value * 2; kernel[1, 1] = 0; kernel[2, 1] = -value * 2;
                kernel[0, 2] = -value / 2; kernel[1, 2] = -value * 2; kernel[2, 2] = -value / 2;
            }
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            resultR = Math.Abs(resultR);
            resultG = Math.Abs(resultG);
            resultB = Math.Abs(resultB);
            return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));
        }
    }
}
