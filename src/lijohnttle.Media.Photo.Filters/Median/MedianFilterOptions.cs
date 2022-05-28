﻿using System;

namespace lijohnttle.Media.Photo.Filters.Median
{
    public class MedianFilterOptions
    {
        private MedianFilterPixelComparer pixelComparer;
        private int matrixSize;

        public MedianFilterOptions()
        {
            WindowSize = 3;
        }

        public MedianFilterPixelComparer PixelComparer
        {
            get => pixelComparer;
            set => pixelComparer = value ?? MedianFilterPixelComparer.Default;
        }

        public int WindowSize
        {
            get => matrixSize;
            set
            {
                if (value < 1 || value % 2 == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "MatrixSize of the median filter must be an odd number greater than 0");
                }

                matrixSize = value;
            }
        }
    }
}
