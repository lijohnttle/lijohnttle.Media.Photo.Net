using lijohnttle.Media.Photo.Core;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace lijohnttle.Media.Photo.Wpf.Extensions
{
    public static class ImageExtensions
    {
        public static BitmapSource ConvertToBitmapSource(this IImage image)
        {
            if (image == null)
            {
                return null;
            }

            var stride = image.Width * 4;
            byte[] array = new byte[stride * image.Height];
            CopyPixels(image, array);

            var bitmapSource = BitmapSource.Create(
                image.Width,
                image.Height,
                96,
                96,
                PixelFormats.Bgra32,
                null,
                array,
                stride);

            return bitmapSource;
        }

        private static void CopyPixels(IImage image, byte[] array)
        {
            int index = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var color = image.GetPixelColor(x, y).AsRgbColor();

                    array[index] = color.Blue;
                    array[index + 1] = color.Green;
                    array[index + 2] = color.Red;
                    array[index + 3] = (byte)(color.Alpha * 255);

                    index += 4;
                }
            }
        }
    }
}
