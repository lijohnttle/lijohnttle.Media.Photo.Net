using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lijohnttle.Media.Photo.Filters.Median.Histograms
{
    /// <summary>
    /// Represents a median filter kernel that uses histograms.
    /// </summary>
    public class MedianFilterHistogramKernel : SquareKernel
    {
        private List<RgbColor> sortedPixels = new List<RgbColor>();
        private readonly MedianFilterHistogram[] histograms;
        private readonly IComparer<IColor> pixelComparer;
        private readonly HistogramsMedianFilterMetrics metrics;

        public MedianFilterHistogramKernel(
            IImage image,
            int radius,
            IComparer<IColor> pixelComparer,
            HistogramsMedianFilterMetrics metrics)
            : base(image, radius, 0, 0)
        {
            histograms = new MedianFilterHistogram[image.Width];
            sortedPixels = new List<RgbColor>((int)Math.Pow(2 * radius + 1, 2));
            this.pixelComparer = pixelComparer;
            this.metrics = metrics;

            MoveTo(0, 0);
        }


        public override IEnumerable<RgbColor> GetPixels()
        {
            for (int x = Left; x <= Right; x++)
            {
                MedianFilterHistogram histogram = histograms[x];

                foreach (RgbColor pixel in histogram.GetPixels())
                {
                    yield return pixel;
                }
            }
        }

        public override bool MoveToNext()
        {
            if (PositionX >= Image.Width - 1 && PositionY >= Image.Height - 1)
            {
                return false;
            }

            if (PositionX < Image.Width - 1)
            {
                // move right
                PositionX++;

                UpdatePaddings();

                var rightHistogram = histograms[Right];

                if (rightHistogram == null || rightHistogram.PositionY != PositionY - 1)
                {
                    if (rightHistogram == null)
                    {
                        rightHistogram = new(Right, PositionY);
                        histograms[Right] = rightHistogram;
                    }

                    rightHistogram.Fill(Image, Radius);
                }
                else
                {
                    rightHistogram.MoveDown(Image, Radius);
                }

                return true;
            }
            else
            {
                // new line
                PositionX = 0;
                PositionY++;

                UpdatePaddings();

                for (int x = Left; x < Right; x++)
                {
                    histograms[x].MoveDown(Image, Radius);
                }

                return true;
            }
        }

        public override void MoveTo(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;

            sortedPixels.Clear();

            UpdatePaddings();

            for (int x = Left; x <= Right; x++)
            {
                MedianFilterHistogram histogram = histograms[x];

                if (histogram == null || histogram.PositionY != positionY - 1)
                {
                    if (histogram == null)
                    {
                        histogram = new(x, PositionY);
                        histograms[x] = histogram;
                    }

                    histogram.Fill(Image, Radius);
                }
                else
                {
                    histogram.MoveDown(Image, Radius);
                }
            }
        }

        public RgbColor FindMedianPixel()
        {
            int middleIndex = Width * Height / 2;

            return GetPixels()
                .OrderBy(t => t, pixelComparer)
                .ElementAt(middleIndex);
        }

        private void Q(IList<RgbColor> pixels)
        {
            int pivotIndex = pixels.Count / 2;
            RgbColor pivot = pixels[pivotIndex];
            int leftIndex = 0;
            int rightIndex = pixels.Count - 1;

            while (leftIndex < rightIndex)
            {
                RgbColor left = pixels[leftIndex];
                RgbColor right = pixels[rightIndex];


            }
        }
    }
}
