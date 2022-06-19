using lijohnttle.Media.Photo.Core.Extensions;

namespace lijohnttle.Media.Photo.Core
{
    public class BitmapImage : IImage
    {
        private readonly RgbColor[,] data;

        /// <param name="data">Array of pixels. First dimention are columns, second dimention are rows</param>
        public BitmapImage(RgbColor[,] data)
        {
            this.data = data;
        }

        public BitmapImage(IImage image)
        {
            this.data = image.CopyRgbPixels();
        }

        public int Width => data.GetLength(0);

        public int Height => data.GetLength(1);

        public IColor GetPixel(int x, int y) => data[x, y];

        public void SetPixel(int x, int y, IColor color)
        {
            data[x, y] = color.AsRgbColor();
        }

        public IColor[,] CopyPixels()
        {
            return data.Clone() as IColor[,];
        }
    }
}
