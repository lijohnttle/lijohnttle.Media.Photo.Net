using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Median.Histograms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Median.Algorithms
{
    public class HistogramsMedianFilterAhgorithm : IMedianFilterAlgorithm
    {
        public static HistogramsMedianFilterAhgorithm Default { get; } = new HistogramsMedianFilterAhgorithm();


        public IImage Apply(IImage source, MedianFilterOptions options)
        {
            IImage result = new BitmapImage(source);
            HistogramsMedianFilterMetrics metrics = new();

            if (source.Width > 0 && source.Height > 0)
            {
                // find all pixels within a kernel
                MedianFilterHistogramKernel kernel = new(source, options.Radius, options.PixelComparer, metrics);

                do
                {
                    // find median pixel
                    IColor medianPixel = kernel.FindMedianPixel();

                    result.SetPixel(kernel.PositionX, kernel.PositionY, medianPixel);

                    metrics.PixelsProcessed++;

                    // move kernel
                } while (kernel.MoveToNext());

                Debug.WriteLine(metrics);
            }

            return result;
        }
    }
}
