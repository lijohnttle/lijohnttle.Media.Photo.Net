using lijohnttle.Media.Photo.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Median
{
    public class MedianFilter : IImageFilter
    {
        private MedianFilterOptions options = new MedianFilterOptions();
        private MedianFilterPixelComparer pixelComparer = new MedianFilterPixelComparer();

        public MedianFilterOptions Options
        {
            get => options;
            set => options = value ?? new MedianFilterOptions();
        }

        public IImage Apply(IImage image)
        {
            RgbColor[,] data = new RgbColor[image.Width, image.Height];

            int filterOffset = Options.WindowSize / 2;

            IteratePixels(image, filterOffset, (x, y) =>
            {
                List<IColor> neighbourPixels = new List<IColor>();

                IterateWindow(filterOffset, (offsetX, offsetY) =>
                {
                    IColor pixel = FindPixel(image, x, y, offsetX, offsetY);
                    
                    neighbourPixels.Add(pixel);
                });

                neighbourPixels.Sort(options.PixelComparer);

                IColor middlePixel = neighbourPixels[filterOffset];

                data[x, y] = middlePixel.AsRgbColor();
            });

            BitmapImage result = new BitmapImage(data);

            return result;
        }

        private void IteratePixels(IImage image, int filterOffset, Action<int, int> action)
        {
            Parallel.For(filterOffset, image.Height - filterOffset - 1, y =>
            {
                Parallel.For(filterOffset, image.Width - filterOffset - 1, x => action(x, y));
            });
        }

        private void IterateWindow(int filterOffset, Action<int, int> action)
        {
            for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
            {
                for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                {
                    action(filterX, filterY);
                }
            }
        }

        private IColor FindPixel(IImage image, int x, int y, int offsetX, int offsetY)
        {
            int linearIndex = image.Width * (y + offsetY) + x + offsetX;

            int newX = linearIndex % image.Width;
            int newY = linearIndex / image.Width;

            return image.GetPixelColor(newX, newY);
        }
    }
}
