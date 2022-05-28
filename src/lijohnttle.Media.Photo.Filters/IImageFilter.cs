using lijohnttle.Media.Photo.Core;

namespace lijohnttle.Media.Photo.Filters
{
    public interface IImageFilter
    {
        IImage Apply(IImage image);
    }
}
