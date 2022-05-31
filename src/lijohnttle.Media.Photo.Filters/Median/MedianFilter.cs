using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Median.Algorithms;

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

        /// <summary>
        /// Gets or sets the algorithm to be used. If not set then the default algorithm will be used.
        /// </summary>
        public IMedianFilterAlgorithm Algorithm { get; set; }


        /// <inheritdoc />
        public IImage Apply(IImage image)
        {
            IMedianFilterAlgorithm algorithm = Algorithm;

            if (algorithm == null)
            {
                algorithm = StandardMedianFilterAlgorithm.Default;
                //algorithm = HuangMedianFilterAlgorithm.Default;
            }

            return algorithm.Apply(image, Options);
        }
    }
}
