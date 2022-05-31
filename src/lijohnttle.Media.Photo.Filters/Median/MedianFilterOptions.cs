using System;

namespace lijohnttle.Media.Photo.Filters.Median
{
    /// <summary>
    /// Median filter options.
    /// </summary>
    public class MedianFilterOptions
    {
        private MedianFilterPixelComparer pixelComparer = MedianFilterPixelComparer.Default;
        private int radius = 3;


        /// <summary>
        /// A comparer that is used to compare pixels to select a median.
        /// </summary>
        public MedianFilterPixelComparer PixelComparer
        {
            get => pixelComparer;
            init => pixelComparer = value ?? MedianFilterPixelComparer.Default;
        }

        /// <summary>
        /// Gets the processing window radius. Must be a positive number.
        /// </summary>
        public int Radius
        {
            get => radius;
            init
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Radius of the filter must be a positive number.");
                }

                radius = value;
            }
        }
    }
}
