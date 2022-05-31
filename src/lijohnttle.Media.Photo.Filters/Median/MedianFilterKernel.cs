using lijohnttle.Media.Photo.Core;
using System;
using System.Collections.Generic;

namespace lijohnttle.Media.Photo.Filters
{
    public class MedianFilterKernel
    {
        private readonly Queue<IColor> pixels = new Queue<IColor>();
        private readonly int radius;
        private IImage image;
        private int leftPadding;
        private int rightPadding;
        private int topPadding;
        private int bottomPadding;


        public MedianFilterKernel(IImage image, int radius, int positionX, int positionY)
        {
            this.image = image;
            this.radius = radius;

            SetPosition(positionX, positionY);
        }


        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public int Count => pixels.Count;

        public int Left => PositionX - leftPadding;

        public int Right => PositionX + rightPadding;

        public int Top => PositionY - topPadding;

        public int Bottom => PositionY + bottomPadding;

        public int Width => leftPadding + rightPadding + 1;

        public int Height => topPadding + bottomPadding + 1;


        public void SetPosition(int positionX, int positionY)
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

        public bool MoveRight()
        {
            if (PositionX >= image.Width - 1)
            {
                return false;
            }

            while (leftPadding >= radius)
            {
                RemoveLeftColumns();

                leftPadding--;
            }

            PositionX++;

            UpdatePaddings();
            TryAppendRightColumn();

            return true;
        }

        public IEnumerable<IColor> GetPixels() => pixels;

        private void UpdatePaddings()
        {
            leftPadding = Math.Min(radius, PositionX);
            rightPadding = Math.Min(radius, image.Width - 1 - PositionX);
            topPadding = Math.Min(radius, PositionY);
            bottomPadding = Math.Min(radius, image.Height - 1 - PositionY);
        }

        private IEnumerable<IColor> IterateColumnByColumn()
        {
            for (int windowX = Left; windowX <= Right; windowX++)
            {
                for (int windowY = Top; windowY <= Bottom; windowY++)
                {
                    yield return image.GetPixel(windowX, windowY);
                }
            }
        }

        private void TryAppendRightColumn()
        {
            int column = PositionX + radius;

            if (column >= image.Width)
            {
                return;
            }

            for (int row = PositionY - topPadding; row < PositionY + bottomPadding + 1; row++)
            {
                pixels.Enqueue(image.GetPixel(column, row));
            }

            return;
        }

        private void RemoveLeftColumns()
        {
            int kernelHeight = Height;

            for (int i = 0; i < kernelHeight; i++)
            {
                pixels.Dequeue();
            }
        }
    }
}
