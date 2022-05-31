using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Tools;
using System.Collections.Generic;

namespace lijohnttle.Media.Photo.Filters
{
    public class MedianFilterKernel : SquareKernel
    {
        private readonly Queue<IColor> pixels = new Queue<IColor>();


        public MedianFilterKernel(IImage image, int radius, int positionX, int positionY)
            : base(image, radius, positionX, positionY)
        {
            MoveTo(positionX, positionY);
        }


        public override int Count => pixels.Count;


        public override IEnumerable<IColor> GetPixels() => pixels;

        public override void MoveTo(int positionX, int positionY)
        {
            pixels.Clear();

            PositionX = positionX;
            PositionY = positionY;

            UpdatePaddings();

            foreach (IColor pixel in IterateColumnByColumn())
            {
                pixels.Enqueue(pixel);
            }
        }

        public override bool MoveRight()
        {
            if (PositionX >= Image.Width - 1)
            {
                return false;
            }

            while (LeftPadding >= Radius)
            {
                RemoveLeftColumn();

                LeftPadding--;
            }

            PositionX++;

            UpdatePaddings();
            TryAppendRightColumn();

            return true;
        }

        /// <summary>
        /// Iterates all pixels in the kernel.
        /// </summary>
        private IEnumerable<IColor> IterateColumnByColumn()
        {
            for (int windowX = Left; windowX <= Right; windowX++)
            {
                for (int windowY = Top; windowY <= Bottom; windowY++)
                {
                    yield return Image.GetPixel(windowX, windowY);
                }
            }
        }

        /// <summary>
        /// Appends right column of pixels to the queue. 
        /// </summary>
        private void TryAppendRightColumn()
        {
            int column = PositionX + Radius;

            if (column >= Image.Width)
            {
                return;
            }

            for (int row = PositionY - TopPadding; row < PositionY + BottomPadding + 1; row++)
            {
                pixels.Enqueue(Image.GetPixel(column, row));
            }

            return;
        }

        /// <summary>
        /// Removes left column of pixels from the queue. 
        /// </summary>
        private void RemoveLeftColumn()
        {
            int kernelHeight = Height;

            for (int i = 0; i < kernelHeight; i++)
            {
                pixels.Dequeue();
            }
        }
    }
}
