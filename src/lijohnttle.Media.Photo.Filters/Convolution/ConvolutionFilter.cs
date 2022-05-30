using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Internal.Helpers;
using System;
using System.Diagnostics;

namespace lijohnttle.Media.Photo.Filters.Convolution
{
    /// <summary>
    /// Convolution image filter.
    /// </summary>
    public class ConvolutionFilter : IImageFilter
    {
        private ConvolutionFilterOptions options = new ConvolutionFilterOptions();


        /// <summary>
        /// Gets or sets options of the filter.
        /// </summary>
        public ConvolutionFilterOptions Options
        {
            get => options;
            set => options = value;
        }


        /// <inheritdoc />
        public IImage Apply(IImage image)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            IImage result = new BitmapImage(image);
            ConvolutionFilterOptions options = Options;

            sw.Stop();
            Debug.WriteLine($"Copy image took: {sw.ElapsedMilliseconds}ms");

            double[,] matrix = options.ConvolutionMatrix;

            if (matrix == null)
            {
                throw new InvalidOperationException("Convolution matrix can't be null.");
            }

            int filterOffset = matrix.GetLength(0) / 2;

            // iterate every pixel of the image
            image.IteratePixelsInParallel((x, y) =>
            {
                double red = 0;
                double green = 0;
                double blue = 0;

                // sum each color component of the pixels within a window,
                // multiplied by the filter matrix factor
                image.IterateMatrixPixels(x, y, filterOffset, (matrixX, matrixY) =>
                {
                    RgbColor color = image.GetPixel(matrixX, matrixY).AsRgbColor();

                    int matrixXIndex = x - matrixX + filterOffset;
                    int matrixYIndex = y - matrixY + filterOffset;

                    double maxtixFactor = matrix[matrixXIndex, matrixYIndex];

                    red += color.Red * maxtixFactor;
                    green += color.Green * maxtixFactor;
                    blue += color.Blue * maxtixFactor;
                });

                // multiply by the filter factor and add bias
                red = red * Options.Factor + options.Bias;
                green = green * Options.Factor + options.Bias;
                blue = blue * Options.Factor + options.Bias;

                // limit to 0..255
                red = Math.Max(0, Math.Min(255, red));
                green = Math.Max(0, Math.Min(255, green));
                blue = Math.Max(0, Math.Min(255, blue));

                result.SetPixel(x, y, new RgbColor((byte)red, (byte)green, (byte)blue));
            });

            return result;
        }
    }
}
