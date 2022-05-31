using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Median;
using System;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Cartoon
{
    public class CartoonFilter : IImageFilter
    {
        private CartoonFilterOptions options = new CartoonFilterOptions();


        public CartoonFilterOptions Options
        {
            get => options;
            set => options = value ?? new CartoonFilterOptions();
        }


        public IImage Apply(IImage image)
        {
            IImageFilter smoothingFilter = GetSmoothingFilter(Options.SmoothingFilterType);

            IImage smoothedImage = smoothingFilter.Apply(image);
            IImage result = new BitmapImage(smoothedImage);

            double outlineMultiplier = (255 - Options.OutlineStrength) / 255.0;

            IteratePixels(smoothedImage, (x, y) =>
            {
                int gradientValue = ComputeGradientValue(smoothedImage, x, y);

                bool exceedsThreshold = gradientValue > Options.Threshold;

                if (!exceedsThreshold)
                {
                    gradientValue = ComputeDiagonalGradientValue(smoothedImage, x, y);

                    exceedsThreshold = gradientValue > Options.Threshold;
                }

                RgbColor pixel = smoothedImage.GetPixel(x, y).AsRgbColor();

                if (exceedsThreshold)
                {
                    //pixel = new RgbColor(0, 0, 0);
                    pixel = new RgbColor(
                        (byte)(pixel.Red * outlineMultiplier),
                        (byte)(pixel.Green * outlineMultiplier),
                        (byte)(pixel.Blue * outlineMultiplier));
                }

                result.SetPixel(x, y, pixel);
            });

            return result;
        }

        private void IteratePixels(IImage image, Action<int, int> action)
        {
            Parallel.For(1, image.Height - 2, y =>
            {
                Parallel.For(1, image.Width - 2, x => action(x, y));
            });
        }

        private int ComputeGradientValue(IImage image, int x, int y)
        {
            int left = x - 1;
            int right = x + 1;
            int top = y - 1;
            int bottom = y + 1;

            int redGradient = Math.Abs(
                image.GetPixel(left, y).AsRgbColor().Red - image.GetPixel(right, y).AsRgbColor().Red
            );

            redGradient += Math.Abs(
                image.GetPixel(x, top).AsRgbColor().Red - image.GetPixel(x, bottom).AsRgbColor().Red
            );

            int greenGradient = Math.Abs(
                image.GetPixel(left, y).AsRgbColor().Green - image.GetPixel(right, y).AsRgbColor().Green
            );

            greenGradient += Math.Abs(
                image.GetPixel(x, top).AsRgbColor().Green - image.GetPixel(x, bottom).AsRgbColor().Green
            );

            int blueGradient = Math.Abs(
                image.GetPixel(left, y).AsRgbColor().Blue - image.GetPixel(right, y).AsRgbColor().Blue
            );

            blueGradient += Math.Abs(
                image.GetPixel(x, top).AsRgbColor().Blue - image.GetPixel(x, bottom).AsRgbColor().Blue
            );

            return redGradient + greenGradient + blueGradient;
        }

        private int ComputeDiagonalGradientValue(IImage image, int x, int y)
        {
            int left = x - 1;
            int right = x + 1;
            int top = y - 1;
            int bottom = y + 1;

            int redGradient = Math.Abs(
                image.GetPixel(left, top).AsRgbColor().Red - image.GetPixel(right, bottom).AsRgbColor().Red
            );

            redGradient += Math.Abs(
                image.GetPixel(right, top).AsRgbColor().Red - image.GetPixel(left, bottom).AsRgbColor().Red
            );

            int greenGradient = Math.Abs(
                image.GetPixel(left, top).AsRgbColor().Red - image.GetPixel(right, bottom).AsRgbColor().Green
            );

            greenGradient += Math.Abs(
                image.GetPixel(right, top).AsRgbColor().Red - image.GetPixel(left, bottom).AsRgbColor().Green
            );

            int blueGradient = Math.Abs(
                image.GetPixel(left, top).AsRgbColor().Red - image.GetPixel(right, bottom).AsRgbColor().Blue
            );

            blueGradient += Math.Abs(
                image.GetPixel(right, top).AsRgbColor().Red - image.GetPixel(left, bottom).AsRgbColor().Blue
            );

            return redGradient;
        }

        private static IImageFilter GetSmoothingFilter(CartoonSmoothingFilterType smoothingFilterType)
        {
            string filterTypeName = Enum.GetName(typeof(CartoonSmoothingFilterType), smoothingFilterType);

            if (filterTypeName.StartsWith("Median"))
            {
                int windowSize = int.Parse(filterTypeName.Substring("Median".Length).Split("x")[0]);

                return new MedianFilter
                {
                    Options = new MedianFilterOptions
                    {
                        Radius = windowSize
                    }
                };
            }
            else
            {
                throw new NotSupportedException($"Filter type '{smoothingFilterType}' is not supported.");
            }
        }
    }
}
