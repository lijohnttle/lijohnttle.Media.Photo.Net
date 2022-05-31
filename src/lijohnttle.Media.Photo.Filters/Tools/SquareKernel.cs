using lijohnttle.Media.Photo.Core;
using System;

namespace lijohnttle.Media.Photo.Filters.Tools
{
    public abstract class SquareKernel : Kernel
    {
        protected SquareKernel(IImage image, int radius, int positionX, int positionY)
            : base(image, positionX, positionY)
        {
            Radius = radius;
        }


        /// <summary>
        /// Gets kernel radius.
        /// </summary>
        public int Radius { get; }

        /// <summary>
        /// Gets X coordinate of the left border of the kernel.
        /// </summary>
        public int Left => PositionX - LeftPadding;

        /// <summary>
        /// Gets X coordinate of the right border of the kernel.
        /// </summary>
        public int Right => PositionX + RightPadding;

        /// <summary>
        /// Gets Y coordinate of the top border of the kernel.
        /// </summary>
        public int Top => PositionY - TopPadding;

        /// <summary>
        /// Gets Y coordinate of the bottom border of the kernel.
        /// </summary>
        public int Bottom => PositionY + BottomPadding;

        /// <summary>
        /// Gets width of the kernel.
        /// </summary>
        public int Width => LeftPadding + RightPadding + 1;

        /// <summary>
        /// Gets height of the kernel.
        /// </summary>
        public int Height => TopPadding + BottomPadding + 1;

        /// <summary>
        /// Gets the number of pixels on the left from the position of the kernel.
        /// </summary>
        protected int LeftPadding { get; set; }

        /// <summary>
        /// Gets the number of pixels on the right from the position of the kernel.
        /// </summary>
        protected int RightPadding { get; set; }

        /// <summary>
        /// Gets the number of pixels on the top from the position of the kernel.
        /// </summary>
        protected int TopPadding { get; set; }

        /// <summary>
        /// Gets the number of pixels on the bottom from the position of the kernel.
        /// </summary>
        protected int BottomPadding { get; set; }


        protected void UpdatePaddings()
        {
            LeftPadding = Math.Min(Radius, PositionX);
            RightPadding = Math.Min(Radius, Image.Width - 1 - PositionX);
            TopPadding = Math.Min(Radius, PositionY);
            BottomPadding = Math.Min(Radius, Image.Height - 1 - PositionY);
        }
    }
}
