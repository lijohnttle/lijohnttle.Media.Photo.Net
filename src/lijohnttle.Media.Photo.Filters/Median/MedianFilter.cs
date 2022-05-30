using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Internal.Helpers;
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

            // iterate every pixel of the image
            image.IteratePixelsInParallel((x, y) =>
            {
                List<IColor> neighbourPixels = new List<IColor>();

                // find all pixels within a window
                image.IterateMatrixPixels(x, y, filterOffset,
                    (windowX, windowY) => neighbourPixels.Add(image.GetPixel(windowX, windowY)));

                // sort pixels
                neighbourPixels.Sort(options.PixelComparer);

                // take middle pixel
                IColor middlePixel = neighbourPixels[neighbourPixels.Count / 2];

                result.SetPixel(x, y, middlePixel);
            });

            return result;
        }
    }
}
