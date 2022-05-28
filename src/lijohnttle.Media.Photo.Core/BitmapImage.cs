using lijohnttle.Media.Photo.Core.Internal;

namespace lijohnttle.Media.Photo.Core
{
    public class BitmapImage : IImage
    {
        private readonly RgbColor[] data;

        public BitmapImage(RgbColor[] data, int imageWidth, int imageHeight)
        {
            ImageVerificationHelper.VerifyImageSizeAndDataAreConsistent(imageWidth, imageHeight, data.Length);

            this.data = data;
            Width = imageWidth;
            Height = imageHeight;
        }

        public int Width { get; }

        public int Height { get; }

        public IColor GetPixelColor(int x, int y)
        {
            ImageVerificationHelper.VerifyXCoordinate(x, Width);
            ImageVerificationHelper.VerifyYCoordinate(y, Height);

            int index = Width * y + x;

            return data[index];
        }
    }
}
