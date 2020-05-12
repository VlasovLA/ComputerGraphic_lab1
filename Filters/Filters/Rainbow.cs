using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    class Rainbow : Filters
    {
        Color HueChange(Color sour, int ch)
        {
            Color res = sour;
            float h = res.GetHue();
            /////  0-40 G->R /////////   40-80  R->B /////////  80-120 B->G         /////    120-160   G->R    /////   160-200   R->B   //////  200-240 B->G
            if (h < 40)
            {
                if (h + ch <= 40)
                {
                    res = Color.FromArgb(res.R, (res.R - res.G) / 40 * ch, res.B);
                    ch -= 40;
                }

                if (h + ch <= 80)
                {
                    res = Color.FromArgb((res.R - res.B) / 40 * ch, res.R, res.B);
                    ch -= 40;
                }

                if (h + ch <= 120)
                {
                    res = Color.FromArgb(res.B, res.G, (res.G - res.B) / 40 * ch);
                    ch -= 40;
                }

            }

            return res;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = sourceColor;
            return resultColor;
        }
    }
}
