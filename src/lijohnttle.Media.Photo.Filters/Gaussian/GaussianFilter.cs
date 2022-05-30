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

            ConvolutionFilterOptions convolutionFilterOptions = new ConvolutionFilterOptions
            {
                Factor = 1,
                Bias = 0,
                ConvolutionMatrix = new double[matrixSize, matrixSize]
            };

            for (int x = 0; x < matrixSize; x++)
            {
                for (int y = 0; y < matrixSize; y++)
                {
                    convolutionFilterOptions.ConvolutionMatrix[x, y] = ComputeConvolutionMatrixItem(x, y);
                }
            }

            return convolutionFilterOptions;
        }

        /// <summary>
        /// Computes Gaussian matrix item.
        /// </summary>
        /// <param name="x">The X index of the item.</param>
        /// <param name="y">The Y index of the item.</param>
        /// <returns>Convolution matrix value.</returns>
        private double ComputeConvolutionMatrixItem(int x, int y)
        {
            int offsetX = Math.Abs(x - options.Radius);
            int offsetY = Math.Abs(y - options.Radius);

            double result = 1.0 / (2.0 * Math.PI) * Math.Pow(Math.E, -(offsetX * offsetX + offsetY * offsetY) / 2);

            return result;
        }
    }
}
