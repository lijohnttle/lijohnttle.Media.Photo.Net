using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.App.Models
{
    public class ImageProcessor : IImageProcessor
    {
        public async Task<IImage> ProcessImageAsync(IImage image, IEnumerable<IImageFilter> filters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IImage processedImage = image;

                foreach (IImageFilter filter in filters)
                {
                    processedImage = filter.Apply(processedImage);
                }

                return processedImage;
            });
        }
    }
}
