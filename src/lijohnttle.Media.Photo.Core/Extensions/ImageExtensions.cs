namespace lijohnttle.Media.Photo.Core.Extensions
{
    public static class ImageExtensions
    {
        public static RgbColor[,] CopyRgbPixels(this IImage image)
        {
            RgbColor[,] data = new RgbColor[image.Width, image.Height];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    data[x, y] = image.GetPixel(x, y).AsRgbColor();
                }
            }

            return data;
        }
    }
}
