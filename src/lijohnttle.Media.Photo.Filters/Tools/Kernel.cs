using lijohnttle.Media.Photo.Core;
using System.Collections.Generic;

namespace lijohnttle.Media.Photo.Filters.Tools
{
    public abstract class Kernel
    {
        protected Kernel(IImage image, int positionX, int positionY)
        {
            Image = image;
            PositionX = positionX;
            PositionY = positionY;
        }


        /// <summary>
        /// Gets the processing image.
        /// </summary>
        public IImage Image { get; }

        /// <summary>
        /// Gets X coordinate of the kernel.
        /// </summary>
        public int PositionX { get; protected set; }

        /// <summary>
        /// Gets Y coordinate of the kernel.
        /// </summary>
        public int PositionY { get; protected set; }

        /// <summary>
        /// Gets the number of pixels in the kernel.
        /// </summary>
        public abstract int Count { get; }


        /// <summary>
        /// Returns pixels of the kernel.
        /// </summary>
        public abstract IEnumerable<IColor> GetPixels();

        /// <summary>
        /// Moves the kernel to another position.
        /// </summary>
        public abstract void MoveTo(int positionX, int positionY);

        /// <summary>
        /// Moves the kernel to the right.
        /// </summary>
        public abstract bool MoveRight();
    }
}
