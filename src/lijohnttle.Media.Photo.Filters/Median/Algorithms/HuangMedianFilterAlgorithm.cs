using lijohnttle.Media.Photo.Core;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Median.Algorithms
{
    public class HuangMedianFilterAlgorithm : IMedianFilterAlgorithm
    {
        public static HuangMedianFilterAlgorithm Default { get; } = new HuangMedianFilterAlgorithm();


        public IImage Apply(IImage source, MedianFilterOptions options)
        {
            IImage result = new BitmapImage(source);

            // iterate every line of pixels of the image
            Parallel.For(0, source.Height - 1, (currentY) =>
            {
                // find all pixels within a kernel
                MedianFilterKernel kernel = new(source, options.Radius, 0, currentY, options.PixelComparer);

                do
                {
                    // take the median
                    IColor medianPixel = kernel.FindMedianPixel();

                    result.SetPixel(kernel.PositionX, kernel.PositionY, medianPixel);

                    // move kernel
                } while (kernel.MoveToNext());
            });

            return result;
        }
    }
}
