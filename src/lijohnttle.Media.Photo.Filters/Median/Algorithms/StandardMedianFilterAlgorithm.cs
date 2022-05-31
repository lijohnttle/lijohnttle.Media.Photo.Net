using lijohnttle.Media.Photo.Core;
using System.Linq;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Median.Algorithms
{
    public class StandardMedianFilterAlgorithm : IMedianFilterAlgorithm
    {
        public static StandardMedianFilterAlgorithm Default { get; } = new StandardMedianFilterAlgorithm();


        public IImage Apply(IImage source, MedianFilterOptions options)
        {
            IImage result = new BitmapImage(source);

            int radius = options.Radius;

            // iterate every pixel of the image
            Parallel.For(0, source.Height - 1, currentY =>
            {
                Parallel.For(0, source.Width - 1, currentX =>
                {
                    // find all pixels within a kernel
                    MedianFilterKernel kernel = new(source, radius, currentX, currentY);

                    // sort pixels and take the median
                    IColor middlePixel = kernel
                        .GetPixels()
                        .OrderBy(t => t, options.PixelComparer)
                        .ElementAt(kernel.Count / 2);

                    result.SetPixel(currentX, currentY, middlePixel);
                });
            });

            return result;
        }
    }
}
