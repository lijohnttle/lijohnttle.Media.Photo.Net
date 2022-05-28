using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Median;

namespace lijohnttle.Media.Photo.Filters.Extensions
{
    public static class ImageExtensions
    {
        public static IImage ApplyMedianFilter(this IImage image, MedianFilterOptions options = null)
        {
            var filter = new MedianFilter();

            if (options != null)
            {
                filter.Options = options;
            }

            return filter.Apply(image);
        }
    }
}
