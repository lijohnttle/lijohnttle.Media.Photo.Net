using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.App.Models
{
    public interface IImageProcessor
    {
        Task<IImage> ProcessImageAsync(IImage image, IEnumerable<IImageFilter> filters);
    }
}
