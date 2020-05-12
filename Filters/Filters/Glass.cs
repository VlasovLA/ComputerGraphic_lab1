using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    class Glass : Filters
    {
        int value;
        public Glass(int v)
        {
            this.value = v;
        }
      
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Random rand = new Random();
            double area = rand.Next(-1, 1);
            int resultX = Clamp((int)(x + area*value), 0, sourceImage.Width - 1);
            int resultY = Clamp((int)(y + area*value), 0, sourceImage.Height - 1);
            return sourceImage.GetPixel(resultX, resultY);
        }
    }
}
