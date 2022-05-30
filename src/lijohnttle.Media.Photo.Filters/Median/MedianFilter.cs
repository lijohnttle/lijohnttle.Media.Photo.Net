using lijohnttle.Media.Photo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Median
{
    public class MedianFilter : IImageFilter
    {
        private MedianFilterOptions options = new MedianFilterOptions();


        public MedianFilterOptions Options
        {
            get => options;
            set => options = value ?? new MedianFilterOptions();
        }


        public IImage Apply(IImage image)
        {
            IImage result = new BitmapImage(image);

            int filterOffset = Options.WindowSize / 2;

            IteratePixels(image, (x, y) =>
            {
                List<IColor> neighbourPixels = FindWindowPixels(image, x, y, filterOffset).ToList();

                neighbourPixels.Sort(options.PixelComparer);

                IColor middlePixel = neighbourPixels[neighbourPixels.Count / 2];

                result.SetPixel(x, y, middlePixel);
            });

            return result;
        }

        private void IteratePixels(IImage image, Action<int, int> action)
        {
            Parallel.For(0, image.Height - 1, y =>
            {
                Parallel.For(0, image.Width - 1, x => action(x, y));
            });
        }

        private IEnumerable<IColor> FindWindowPixels(IImage image, int x, int y, int filterOffset)
        {
            for (int windowY = Math.Max(0, y - filterOffset); windowY <= Math.Min(image.Height - 1, y + filterOffset); windowY++)
            {
                for (int windowX = Math.Max(0, x - filterOffset); windowX <= Math.Min(image.Width - 1, x + filterOffset); windowX++)
                {
                    yield return image.GetPixel(windowX, windowY);
                }
            }
        }
    }
}
