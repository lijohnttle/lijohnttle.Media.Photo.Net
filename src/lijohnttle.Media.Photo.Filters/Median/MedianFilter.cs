using lijohnttle.Media.Photo.Core;
using System;
using System.Collections.Generic;
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
                List<IColor> neighbourPixels = new List<IColor>();

                IterateWindow(x, y, image.Width, image.Height, filterOffset, (windowX, windowY) =>
                {
                    IColor pixel = image.GetPixel(windowX, windowY);
                    
                    neighbourPixels.Add(pixel);
                });

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

        private void IterateWindow(int x, int y, int width, int height, int filterOffset, Action<int, int> action)
        {
            for (int windowY = Math.Max(0, y - filterOffset); windowY <= Math.Min(height - 1, y + filterOffset); windowY++)
            {
                for (int windowX = Math.Max(0, x - filterOffset); windowX <= Math.Min(width - 1, x + filterOffset); windowX++)
                {
                    action(windowX, windowY);
                }
            }
        }
    }
}
