using lijohnttle.Media.Photo.Core;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Median.Algorithms
{
    public class StandardMedianFilterAlgorithm : IMedianFilterAlgorithm
    {
        public static StandardMedianFilterAlgorithm Default { get; } = new StandardMedianFilterAlgorithm();


        public IImage Apply(IImage source, MedianFilterOptions options)
        {
            IImage result = new BitmapImage(source);

            // iterate every pixel of the image
            Parallel.For(0, source.Height - 1, currentY =>
            {
                Parallel.For(0, source.Width - 1, currentX =>
                {
                    // find all pixels within a kernel
                    MedianFilterKernel kernel = new(source, options.Radius, currentX, currentY, options.PixelComparer);

                    // take the median
                    IColor middlePixel = kernel.FindMedianPixel();

                    result.SetPixel(currentX, currentY, middlePixel);
                });
            });

            return result;
        }
    }
}
