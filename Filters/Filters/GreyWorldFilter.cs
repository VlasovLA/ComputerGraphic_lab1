using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Filters
{
    class GreyWorldFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return Color.Blue;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int AvgR = 0;
            int AvgG = 0;
            int AvgB = 0;
            int AvgAll;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color tmp;
                    tmp = sourceImage.GetPixel(i, j);
                    AvgR += tmp.R;
                    AvgG += tmp.G;
                    AvgB += tmp.B;
                }
            }
            int resol = sourceImage.Width * sourceImage.Height;
            AvgR = AvgR / resol;
            AvgG = AvgG / resol;
            AvgB = AvgB / resol;
            AvgAll = (AvgR + AvgG + AvgB) / 3;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)(50 + (float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    sourceColor = Color.FromArgb(Clamp(sourceColor.R * AvgAll / AvgR, 0, 255), Clamp(sourceColor.G * AvgAll / AvgG, 0, 255), Clamp(sourceColor.B * AvgAll / AvgB, 0, 255));
                    resultImage.SetPixel(i, j, sourceColor);

                }
            }

            return resultImage;
        }

    }
}
