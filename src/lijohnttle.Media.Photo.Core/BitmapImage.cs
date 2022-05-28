using lijohnttle.Media.Photo.Core.Internal;

namespace lijohnttle.Media.Photo.Core
{
    public class BitmapImage : IImage
    {
        private readonly RgbColor[,] data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Array of pixels. First dimention are columns, second dimention are rows</param>
        public BitmapImage(RgbColor[,] data)
        {
            this.data = data;
        }

        public BitmapImage(IImage image)
        {
            RgbColor[,] data = new RgbColor[image.Width, image.Height];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    data[x, y] = image.GetPixelColor(x, y).AsRgbColor();
                }
            }

            this.data = data;
        }

        public int Width => data.GetLength(0);

        public int Height => data.GetLength(1);

        public IColor GetPixelColor(int x, int y)
        {
            ImageVerificationHelper.VerifyXCoordinate(x, Width);
            ImageVerificationHelper.VerifyYCoordinate(y, Height);

            return data[x, y];
        }
    }
}
