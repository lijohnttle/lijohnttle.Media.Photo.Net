using lijohnttle.Media.Photo.Core;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace lijohnttle.Media.Photo.Wpf.Extensions
{
    public static class BitmapSourceExtensions
    {
        public static IImage ConvertToImage(this BitmapSource bitmapSource)
        {
            int bytesPerPixel = (bitmapSource.Format.BitsPerPixel + 7) / 8;

            RgbColor[,] data = new RgbColor[bitmapSource.PixelWidth, bitmapSource.PixelHeight];

            for (int x = 0; x < bitmapSource.PixelWidth; x++)
            {
                for (int y = 0; y < bitmapSource.PixelHeight; y++)
                {
                    byte[] pixel = new byte[bytesPerPixel];
                    bitmapSource.CopyPixels(new Int32Rect(x, y, 1, 1), pixel, bytesPerPixel, 0);
                    data[x, y] = CreateRgbColor(pixel, bitmapSource.Format);
                }
            }

            return new Core.BitmapImage(data);
        }

        private static RgbColor CreateRgbColor(byte[] pixel, PixelFormat pixelFormat)
        {
            if (pixelFormat == PixelFormats.Bgr32)
            {
                return new RgbColor(pixel[2], pixel[1], pixel[0], 1);
            }
            else if (pixelFormat == PixelFormats.Bgra32)
            {
                return new RgbColor(pixel[2], pixel[1], pixel[0], (float)pixel[3] / 255);
            }
            else
            {
                throw new NotSupportedException($"PixelFormat '{pixelFormat}' is not supported");
            }
        }
    }
}
