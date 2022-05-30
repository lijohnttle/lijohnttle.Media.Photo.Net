using System;

namespace lijohnttle.Media.Photo.Filters.Convolution
{
    /// <summary>
    /// Convolution filter options.
    /// </summary>
    public class ConvolutionFilterOptions
    {
        private double[,] convolutionMatrix;


        /// <summary>
        /// The convolution matrix of the filter. It should be a square matrix with
        /// the size that is an odd number >= 3.
        /// </summary>
        public double[,] ConvolutionMatrix
        {
            get => convolutionMatrix;
            init
            {
                if (value != null)
                {
                    if (value.GetLength(0) != value.GetLength(1))
                    {
                        throw new ArgumentException(nameof(value),
                            "The number of rows and columns in a convolution matrix must be equal");
                    }

                    if (value.GetLength(0) < 3 || value.GetLength(0) % 2 == 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value),
                            "Convolution matrix size must be an odd number >= 3");
                    }
                }

                convolutionMatrix = value;
            }
        }

        /// <summary>
        /// Convolution filter factor.
        /// </summary>
        public double Factor { get; init; } = 1;

        /// <summary>
        /// Convolution filter bias.
        /// </summary>
        public double Bias { get; init; }
    }
}
