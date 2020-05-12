using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Filters
{
    class GlowingBorders : MatrixFilters
    {
        int value;
        public GlowingBorders(int val)
        {
            this.value = val;
            kernel = new float[3, 3];

            kernel[0, 0] = value / 2; kernel[1, 0] = value * 2; kernel[2, 0] = -value / 2;
            kernel[0, 1] = value * 2; kernel[1, 1] = 0; kernel[2, 1] = -value * 2;
            kernel[0, 2] = value / 2; kernel[1, 2] = -value * 2; kernel[2, 2] = -value / 2;
        }


        new public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 33));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    int length = 9;
                    int a = 0;
                    int[] arr = new int[length];
                    Color[] col = new Color[length];
                    for (int l = -1; l <= 1; l++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            int idX = Clamp(i + k, 0, sourceImage.Width - 1);
                            int idY = Clamp(j + l, 0, sourceImage.Height - 1);
                            Color neighborColor = sourceImage.GetPixel(idX, idY);
                            arr[a] = (neighborColor.R + neighborColor.G + neighborColor.B) / 3;
                            col[a] = neighborColor;
                            a++;
                        }
                    }
                    Array.Sort(arr);
                    for (int t = 0; t < length; t++)
                        if (arr[4] == (col[t].R + col[t].G + col[t].B) / 3)
                        {
                            resultImage.SetPixel(i, j, col[t]);
                            break;
                        }
                }
            }

            //////////////////////////////

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)(33 + (float)i / resultImage.Width * 33));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    int radiusX = kernel.GetLength(0) / 2;
                    int radiusY = kernel.GetLength(1) / 2;

                    float resultR = 0;
                    float resultG = 0;
                    float resultB = 0;
                    for (int l = -radiusY; l <= radiusY; l++)
                        for (int k = -radiusX; k <= radiusX; k++)
                        {
                            int idX = Clamp(i + k, 0, sourceImage.Width - 1);
                            int idY = Clamp(j + l, 0, sourceImage.Height - 1);
                            Color neighborColor = sourceImage.GetPixel(idX, idY);
                            resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                            resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                            resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                        }
                    Color res = Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));
                    resultImage.SetPixel(i, j, res);
                }
            }

            ////////////////////////////////

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)(66 + (float)i / resultImage.Width * 34));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    int br = 0;
                    Color col = Color.Black;
                    for (int l = -1; l <= 1; l++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            int idX = Clamp(i + k, 0, sourceImage.Width - 1);
                            int idY = Clamp(j + l, 0, sourceImage.Height - 1);
                            Color neighborColor = sourceImage.GetPixel(idX, idY);
                            int curBr = (int)(neighborColor.R * 0.3 + neighborColor.G * 0.59 + neighborColor.B * 0.11);
                            if (br < curBr)
                            {
                                br = (int)(neighborColor.R * 0.3 + neighborColor.G * 0.59 + neighborColor.B * 0.11);
                                col = neighborColor;
                            }
                        }
                    }
                    resultImage.SetPixel(i, j, col);
                }
            }

            return resultImage;
        }

    }
}
