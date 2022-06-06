using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Tools;
using System.Collections.Generic;
using System.Linq;

namespace lijohnttle.Media.Photo.Filters
{
    /// <summary>
    /// Represents a median filter kernel.
    /// </summary>
    public class MedianFilterKernel : SquareKernel
    {
        private readonly Queue<RgbColor> pixels = new Queue<RgbColor>();
        private readonly IComparer<IColor> pixelComparer;

        public MedianFilterKernel(IImage image, int radius, int positionX, int positionY, IComparer<IColor> pixelComparer)
            : base(image, radius, positionX, positionY)
        {
            this.pixelComparer = pixelComparer;
         
            MoveTo(positionX, positionY);
        }


        public override IEnumerable<RgbColor> GetPixels() => pixels;

        public override void MoveTo(int positionX, int positionY)
        {
            pixels.Clear();

            PositionX = positionX;
            PositionY = positionY;

            UpdatePaddings();

            foreach (RgbColor pixel in IterateColumnByColumn())
            {
                pixels.Enqueue(pixel);
            }
        }

        public override bool MoveToNext()
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

        public RgbColor FindMedianPixel()
        {


            return pixels
                .OrderBy(t => t, pixelComparer)
                .ElementAt(pixels.Count / 2);
        }

        /// <summary>
        /// Iterates all pixels in the kernel.
        /// </summary>
        private IEnumerable<RgbColor> IterateColumnByColumn()
        {
            for (int windowX = Left; windowX <= Right; windowX++)
            {
                for (int windowY = Top; windowY <= Bottom; windowY++)
                {
                    yield return Image.GetPixel(windowX, windowY).AsRgbColor();
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
                pixels.Enqueue(Image.GetPixel(column, row).AsRgbColor());
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
