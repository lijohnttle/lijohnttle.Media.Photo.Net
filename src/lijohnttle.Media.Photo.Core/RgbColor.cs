using lijohnttle.Media.Photo.Core.Internal;
using System;

namespace lijohnttle.Media.Photo.Core
{
    /// <summary>
    /// Rgb color.
    /// </summary>
    public struct RgbColor : IColor, IEquatable<RgbColor>
    {
        private readonly float alpha;
        private readonly byte red;
        private readonly byte green;
        private readonly byte blue;


        public RgbColor(byte red, byte green, byte blue, float alpha = 1)
            : this()
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }


        /// <summary>
        /// Gets the red component of a color;
        /// </summary>
        public byte Red
        {
            get => red;
            init
            {
                ColorVerificationHelper.VerifyRgbColorComponent(value);

                red = value;
            }
        }

        /// <summary>
        /// Gets the green component of a color.
        /// </summary>
        public byte Green
        {
            get => green;
            init
            {
                ColorVerificationHelper.VerifyRgbColorComponent(value);

                green = value;
            }
        }

        /// <summary>
        /// Gets the blue component of a color.
        /// </summary>
        public byte Blue
        {
            get => blue;
            init
            {
                ColorVerificationHelper.VerifyRgbColorComponent(value);

                blue = value;
            }
        }

        /// <summary>
        /// Gets an alpha channel value. Valid values are in the range from 0 (fully transparent) to 1 (fully opaque).
        /// </summary>
        public float Alpha
        {
            get => alpha;
            init
            {
                ColorVerificationHelper.VerifyAlphaChannel(value);

                alpha = value;
            }
        }


        public RgbColor AsRgbColor() => this;

        public override bool Equals(object obj)
        {
            if (obj is RgbColor rgbColor)
            {
                return Equals(rgbColor);
            }

            return false;
        }

        public bool Equals(RgbColor other)
        {
            return
                Red == other.Red &&
                Green == other.Green &&
                Blue == other.Blue &&
                Alpha.Equals(other.Alpha);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Red, Green, Blue, Alpha);
        }
    }
}
