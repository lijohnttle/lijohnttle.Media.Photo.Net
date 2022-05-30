using System;

namespace lijohnttle.Media.Photo.Filters.Median
{
    /// <summary>
    /// Median filter options.
    /// </summary>
    public class MedianFilterOptions
    {
        private MedianFilterPixelComparer pixelComparer = MedianFilterPixelComparer.Default;
        private int windowSize = 3;


        /// <summary>
        /// A comparer that is used to compare pixels to select a median.
        /// </summary>
        public MedianFilterPixelComparer PixelComparer
        {
            get => pixelComparer;
            init => pixelComparer = value ?? MedianFilterPixelComparer.Default;
        }

        /// <summary>
        /// Gets the processing window size. Must be an odd number >= 3.
        /// </summary>
        public int WindowSize
        {
            get => windowSize;
            init
            {
                if (value < 3 || value % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Window size of the filter must be an odd number >= 3.");
                }

                windowSize = value;
            }
        }
    }
}
