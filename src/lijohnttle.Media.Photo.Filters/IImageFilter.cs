using lijohnttle.Media.Photo.Core;

namespace lijohnttle.Media.Photo.Filters
{
    /// <summary>
    /// Abstraction of an image filter.
    /// </summary>
    public interface IImageFilter
    {
        /// <summary>
        /// Applies filter to an image.
        /// </summary>
        /// <param name="image">The image that should be processed.</param>
        /// <returns>A new processed image.</returns>
        IImage Apply(IImage image);
    }
}
