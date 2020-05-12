using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    class MedianFilter : Filters
    {
        public MedianFilter()
        {
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int length = 9;
            int a = 0;
            int[] br = new int[length];
            Color[] col = new Color[length];
            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    br[a] = (int)((float)neighborColor.R * 0.3 + (float)neighborColor.G * 0.59 + (float)neighborColor.B * 0.11);
                    col[a] = neighborColor;
                    a++;
                }
            }
            Array.Sort(br);
            for (int i = 0; i < length; i++)
                if (br[4] == (int)((float)col[i].R * 0.3 + (float)col[i].G * 0.59 + (float)col[i].B * 0.11))
                    return col[i];
            return Color.Red;
        }
    }
}
