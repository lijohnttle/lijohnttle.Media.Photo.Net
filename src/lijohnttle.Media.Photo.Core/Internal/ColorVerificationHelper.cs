using System;

namespace lijohnttle.Media.Photo.Core.Internal
{
    public static class ColorVerificationHelper
    {
        public static void VerifyRgbColorComponent(int componentValue)
        {
            if (componentValue < 0 || componentValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(componentValue), "Color component is out of range.");
            }
        }

        public static void VerifyAlphaChannel(float alpha)
        {
            if (alpha < 0 || alpha > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(alpha), "Alpha channel value is out of range.");
            }
        }
    }
}
