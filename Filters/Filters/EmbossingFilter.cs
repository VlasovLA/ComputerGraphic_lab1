using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Filters
{
    class EmbossingFilter : Filters
    {
        protected float[,] kernel = null;
        public EmbossingFilter()
        {
            kernel = new float[3, 3];
            kernel[0, 0] = 0; kernel[1, 0] = 1; kernel[2, 0] = 0;
            kernel[0, 1] = 1; kernel[1, 1] = 0; kernel[2, 1] = -1;
            kernel[0, 2] = 0; kernel[1, 2] = -1; kernel[2, 2] = 0;
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
            return Color.FromArgb(Clamp((int)((resultR + 255) / 2), 0, 255), Clamp((int)((resultG + 255) / 2), 0, 255), Clamp((int)((resultB + 255) / 2), 0, 255));
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    int Intensity = (int)(0.2126 * sourceColor.R + 0.7152 * sourceColor.G + 0.0722 * sourceColor.B);
                    Color resultColor = Color.FromArgb(Intensity, Intensity, Intensity);
                    resultImage.SetPixel(i, j, resultColor);
                }
            }

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
            }

            

            return resultImage;
        }

    }
}
