using lijohnttle.Media.Photo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Median
{
    /// <summary>
    /// Median image filter.
    /// </summary>
    public class MedianFilter : IImageFilter
    {
        private MedianFilterOptions options = new MedianFilterOptions();


        /// <summary>
        /// Gets or sets options of the filter.
        /// </summary>
        public MedianFilterOptions Options
        {
            get => options;
            set => options = value ?? new MedianFilterOptions();
        }


        /// <inheritdoc />
        public IImage Apply(IImage image)
        {
            IImage result = new BitmapImage(image);

            int filterOffset = Options.WindowSize / 2;

            IteratePixels(image, (x, y) =>
            {
                List<IColor> neighbourPixels = FindWindowPixels(image, x, y, filterOffset).ToList();

                neighbourPixels.Sort(options.PixelComparer);

                IColor middlePixel = neighbourPixels[neighbourPixels.Count / 2];

                result.SetPixel(x, y, middlePixel);
            });

            return result;
        }


        /// <summary>
        /// Iterates image pixels.
        /// </summary>
        /// <param name="image">The original image that is being processed.</param>
        /// <param name="action">An action to process pixel.</param>
        private void IteratePixels(IImage image, Action<int, int> action)
        {
            Parallel.For(0, image.Height - 1, y =>
            {
                Parallel.For(0, image.Width - 1, x => action(x, y));
            });
        }

        /// <summary>
        /// Finds all pixels inside processing window.
        /// </summary>
        /// <param name="image">The original image that is being processed.</param>
        /// <param name="x">The X index of the current pixel.</param>
        /// <param name="y">The Y index of the current pixel.</param>
        /// <param name="filterOffset">The half-length of the processing window.</param>
        /// <returns>List of pixels.</returns>
        private IEnumerable<IColor> FindWindowPixels(IImage image, int x, int y, int filterOffset)
        {
            for (int windowY = Math.Max(0, y - filterOffset); windowY <= Math.Min(image.Height - 1, y + filterOffset); windowY++)
            {
                for (int windowX = Math.Max(0, x - filterOffset); windowX <= Math.Min(image.Width - 1, x + filterOffset); windowX++)
                {
                    yield return image.GetPixel(windowX, windowY);
                }
            }
        }
    }
}
