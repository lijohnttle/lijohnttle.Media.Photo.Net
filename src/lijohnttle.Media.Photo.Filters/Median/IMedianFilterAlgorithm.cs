using lijohnttle.Media.Photo.Core;

namespace lijohnttle.Media.Photo.Filters.Median
{
    public interface IMedianFilterAlgorithm
    {
        IImage Apply(IImage source, MedianFilterOptions options);
    }
}
