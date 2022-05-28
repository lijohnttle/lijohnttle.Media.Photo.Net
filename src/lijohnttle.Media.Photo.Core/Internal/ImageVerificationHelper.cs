using System;

namespace lijohnttle.Media.Photo.Core.Internal
{
    internal static class ImageVerificationHelper
    {
        public static void VerifyImageSizeAndDataAreConsistent(int imageWidth, int imageHeight, int dataSize)
        {
            if (imageWidth * imageHeight != dataSize)
            {
                throw new InvalidOperationException("Image size is not consistent with the data array size.");
            }
        }

        public static void VerifyXCoordinate(int x, int imageWidth)
        {
            if (x < 0 || x >= imageWidth)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "X coordinate is out of range of the image.");
            }
        }

        public static void VerifyYCoordinate(int y, int imageHeight)
        {
            if (y < 0 || y >= imageHeight)
            {
                throw new ArgumentOutOfRangeException(nameof(y), "Y coordinate is out of range of the image.");
            }
        }
    }
}
