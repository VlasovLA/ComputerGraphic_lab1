using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Filters
{
    class HistogramSretching : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return Color.Blue;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int minBr = 255;
            int maxBr = 0;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color tmp;
                    tmp = sourceImage.GetPixel(i, j);
                    int curBr = (int)(tmp.R * 0.3 + tmp.G * 0.59 + tmp.B * 0.11);
                    if (curBr < minBr)
                        minBr = curBr;
                    if (curBr > maxBr)
                        maxBr = curBr;
                }
            }

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)(50 + (float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    int sourceBr = (int)(sourceColor.R * 0.3 + sourceColor.G * 0.59 + sourceColor.B * 0.11);
                    int resultBr = (int)((float)(sourceBr - minBr) * 255 / (float)(maxBr - minBr + 1));
                    float kf = (float)resultBr / (float)(sourceBr + 1);

                    Color resultColor = Color.FromArgb(Clamp((int)(sourceColor.R * kf), 0, 255), Clamp((int)(sourceColor.G * kf), 0, 255), Clamp((int)(sourceColor.B * kf), 0, 255));
                    resultImage.SetPixel(i, j, resultColor);

                }
            }

            return resultImage;
        }

    }
}
