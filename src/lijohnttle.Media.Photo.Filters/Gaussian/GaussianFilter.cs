using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Convolution;
using System;

namespace lijohnttle.Media.Photo.Filters.Gaussian
{
    public class GaussianFilter : IImageFilter
    {
        private readonly ConvolutionFilter _convolutionFilter;
        private GaussianFilterOptions options = new GaussianFilterOptions();


        public GaussianFilter()
        {
            _convolutionFilter = new ConvolutionFilter();
        }


        /// <summary>
        /// Gets or sets options of the filter.
        /// </summary>
        public GaussianFilterOptions Options
        {
            get => options;
            set
            {
                options = value ?? new GaussianFilterOptions();

                _convolutionFilter.Options = BuildConvolutionOptions();
            }
        }


        /// <inheritdoc />
        public IImage Apply(IImage image)
        {
            return _convolutionFilter.Apply(image);
        }

        /// <summary>
        /// Creates a convolution filter options.
        /// </summary>
        /// <returns>Convolution filter options.</returns>
        private ConvolutionFilterOptions BuildConvolutionOptions()
        {
            int matrixSize = options.Radius * 2 + 1;
            double weight = options.Weight;

            ConvolutionFilterOptions convolutionFilterOptions = new ConvolutionFilterOptions
            {
                Factor = 1,
                Bias = 0,
                ConvolutionMatrix = new double[matrixSize, matrixSize]
            };

            double calculatedEuler = 1.0 / (2.0 * Math.PI * Math.Pow(weight, 2));

            for (int x = 0; x < matrixSize; x++)
            {
                for (int y = 0; y < matrixSize; y++)
                {
                    convolutionFilterOptions.ConvolutionMatrix[x, y] = ComputeConvolutionMatrixItem(x, y, calculatedEuler, weight);
                }
            }

            double currentSum = SumMatrixValues(convolutionFilterOptions.ConvolutionMatrix);

            CoerceMatrixValues(currentSum, convolutionFilterOptions.ConvolutionMatrix);

            currentSum = SumMatrixValues(convolutionFilterOptions.ConvolutionMatrix);

            return convolutionFilterOptions;


            double SumMatrixValues(double[,] matrix)
            {
                double currentSum = 0;
                int matrixSize = matrix.GetLength(0);

                for (int x = 0; x < matrixSize; x++)
                {
                    for (int y = 0; y < matrixSize; y++)
                    {
                        currentSum += matrix[x, y];
                    }
                }

                return currentSum;
            }

            void CoerceMatrixValues(double currentSum, double[,] matrix)
            {
                int matrixSize = matrix.GetLength(0);

                if (Math.Abs(1 - currentSum) > 0.001)
                {
                    double matrixMultiplier = 1 / currentSum;

                    for (int x = 0; x < matrixSize; x++)
                    {
                        for (int y = 0; y < matrixSize; y++)
                        {
                            matrix[x, y] *= matrixMultiplier;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Computes Gaussian matrix item.
        /// </summary>
        /// <param name="x">The X index of the item.</param>
        /// <param name="y">The Y index of the item.</param>
        /// <returns>Convolution matrix value.</returns>
        private double ComputeConvolutionMatrixItem(int x, int y, double calculatedEuler, double factor)
        {
            int offsetX = Math.Abs(x - options.Radius);
            int offsetY = Math.Abs(y - options.Radius);

            double distance = (offsetX * offsetX + offsetY * offsetY) / (2.0 * factor * factor);

            double result = calculatedEuler * Math.Pow(Math.E, -distance);

            return result;
        }
    }
}
