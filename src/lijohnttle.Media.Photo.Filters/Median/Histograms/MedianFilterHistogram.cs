using lijohnttle.Media.Photo.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lijohnttle.Media.Photo.Filters.Median.Histograms
{
    /// <summary>
    /// Represents a histogram.
    /// </summary>
    public class MedianFilterHistogram
    {
        private readonly Queue<RgbColor> pixels;


        public MedianFilterHistogram(int currentX, int currentY)
        {
            pixels = new Queue<RgbColor>();
            PositionX = currentX;
            PositionY = currentY;
        }


        public int PositionX { get; }

        public int PositionY { get; private set; }


        public List<RgbColor> GetPixels() => pixels.ToList();

        /// <summary>
        /// Moves histogram of the height equal to the radius down 1 pixel.
        /// </summary>
        /// <param name="image">Source image.</param>
        /// <param name="radius">Radius of the median filter.</param>
        /// <returns></returns>
        public bool MoveDown(IImage image, int radius)
        {
            if (PositionY >= image.Height - 1)
            {
                return false;
            }

            if (PositionY >= radius)
            {
                pixels.Dequeue();
            }

            PositionY++;

            if (PositionY < image.Height - radius)
            {
                pixels.Enqueue(image.GetPixel(PositionX, PositionY).AsRgbColor());
            }

            return true;
        }

        public void Fill(IImage image, int radius)
        {
            pixels.Clear();

            int top = Math.Max(0, PositionY - radius);
            int bottom = Math.Min(image.Height - 1, PositionY + radius);

            for (int y = top; y <= bottom; y++)
            {
                pixels.Enqueue(image.GetPixel(PositionX, y).AsRgbColor());
            }
        }
    }
}
