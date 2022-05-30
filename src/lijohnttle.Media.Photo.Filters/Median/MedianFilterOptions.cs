using System;

namespace lijohnttle.Media.Photo.Filters.Median
{
    /// <summary>
    /// Median filter options.
    /// </summary>
    public class MedianFilterOptions
    {
        private MedianFilterPixelComparer pixelComparer;
        private int windowSize;

        public MedianFilterOptions()
        {
            WindowSize = 3;
            PixelComparer = MedianFilterPixelComparer.Default;
        }


        /// <summary>
        /// A comparer that is used to compare pixels to select a median.
        /// </summary>
        public MedianFilterPixelComparer PixelComparer
        {
            get => pixelComparer;
            init => pixelComparer = value ?? MedianFilterPixelComparer.Default;
        }

        /// <summary>
        /// A processing window size. Must be an odd number >= 3.
        /// </summary>
        public int WindowSize
        {
            get => windowSize;
            init
            {
                if (value < 3 || value % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Window size of the median filter must be an odd number >= 3");
                }

                windowSize = value;
            }
        }
    }
}
