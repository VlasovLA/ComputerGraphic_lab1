using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Filters
{
    class Closing : Filters
    {
        protected float[,] kernel = null;
        public Closing(int mode)
        {
            if (mode == 0)
            {
                kernel = new float[3, 3];
                kernel[0, 0] = 0; kernel[0, 1] = 1; kernel[0, 2] = 0;
                kernel[1, 0] = 1; kernel[1, 1] = 1; kernel[1, 2] = 1;
                kernel[2, 0] = 0; kernel[2, 1] = 1; kernel[2, 2] = 0;
            }
            if (mode == 1)
            {
                kernel = new float[3, 3];
                kernel[0, 0] = 1; kernel[0, 1] = 1; kernel[0, 2] = 1;
                kernel[1, 0] = 1; kernel[1, 1] = 1; kernel[1, 2] = 1;
                kernel[2, 0] = 1; kernel[2, 1] = 1; kernel[2, 2] = 1;
            }
            if (mode == 2)
            {
                kernel = new float[7, 7];
                kernel[0, 0] = 0; kernel[0, 1] = 0; kernel[0, 2] = 1; kernel[0, 3] = 1; kernel[0, 4] = 1; kernel[0, 5] = 0; kernel[0, 6] = 0;
                kernel[1, 0] = 0; kernel[1, 1] = 1; kernel[1, 2] = 1; kernel[1, 3] = 1; kernel[1, 4] = 1; kernel[1, 5] = 1; kernel[1, 6] = 0;
                kernel[2, 0] = 1; kernel[2, 1] = 1; kernel[2, 2] = 1; kernel[2, 3] = 1; kernel[2, 4] = 1; kernel[2, 5] = 1; kernel[2, 6] = 1;
                kernel[3, 0] = 1; kernel[3, 1] = 1; kernel[3, 2] = 1; kernel[3, 3] = 1; kernel[3, 4] = 1; kernel[3, 5] = 1; kernel[3, 6] = 1;
                kernel[4, 0] = 1; kernel[4, 1] = 1; kernel[4, 2] = 1; kernel[4, 3] = 1; kernel[4, 4] = 1; kernel[4, 5] = 1; kernel[4, 6] = 1;
                kernel[5, 0] = 0; kernel[5, 1] = 1; kernel[5, 2] = 1; kernel[5, 3] = 1; kernel[5, 4] = 1; kernel[5, 5] = 1; kernel[5, 6] = 0;
                kernel[6, 0] = 0; kernel[6, 1] = 0; kernel[6, 2] = 1; kernel[6, 3] = 1; kernel[6, 4] = 1; kernel[6, 5] = 0; kernel[6, 6] = 0;
            }
        }
        public Closing(float[,] kern)
        {
            kernel = kern;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return Color.Yellow;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int MW = kernel.GetLength(0) / 2;
            int MH = kernel.GetLength(1) / 2;

            for (int y = MH; y < sourceImage.Height - MH; y++)
            {
                worker.ReportProgress((int)((float)(y - MH) / (sourceImage.Height + MH) * 50));
                if (worker.CancellationPending)
                    return null;
                for (int x = MW; x < sourceImage.Width - MW; x++)
                {
                    float max = 0;
                    Color maxC = Color.Black;
                    for (int j = -MH; j <= MH; j++)
                        for (int i = -MW; i <= MW; i++)
                        {
                            Color src = sourceImage.GetPixel(x + i, y + j);
                            if ((kernel[i + MW, j + MH] > 0) && ((src.R * 0.3 + src.G * 0.59 + src.B * 0.11) > max))
                            {
                                max = src.R * 0.3f + src.G * 0.59f + src.B * 0.11f;
                                maxC = src;
                            }
                        }
                    resultImage.SetPixel(x, y, maxC);
                }
            }

            for (int y = MH; y < sourceImage.Height - MH; y++)
            {
                worker.ReportProgress(50 + (int)((float)(y - MH) / (sourceImage.Height + MH) * 50));
                if (worker.CancellationPending)
                    return null;
                for (int x = MW; x < sourceImage.Width - MW; x++)
                {
                    float min = 255;
                    Color minC = Color.White;
                    for (int j = -MH; j <= MH; j++)
                        for (int i = -MW; i <= MW; i++)
                        {
                            Color src = sourceImage.GetPixel(x + i, y + j);
                            if ((kernel[i + MW, j + MH] > 0) && ((src.R * 0.3 + src.G * 0.59 + src.B * 0.11) < min))
                            {
                                min = src.R * 0.3f + src.G * 0.59f + src.B * 0.11f;
                                minC = src;
                            }
                        }
                    resultImage.SetPixel(x, y, minC);
                }
            }
            return resultImage;
        }

    }
}
