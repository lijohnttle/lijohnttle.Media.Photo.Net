using lijohnttle.Media.Photo.Core;
using System;
using System.Collections.Generic;

namespace lijohnttle.Media.Photo.Filters.Median
{
    /// <summary>
    /// Median filter options.
    /// </summary>
    public class MedianFilterOptions
    {
        private IComparer<RgbColor> pixelComparer = MedianFilterPixelComparer.Default;
        private int radius = 3;


        /// <summary>
        /// A comparer that is used to compare pixels to select a median.
        /// </summary>
        public IComparer<RgbColor> PixelComparer
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
