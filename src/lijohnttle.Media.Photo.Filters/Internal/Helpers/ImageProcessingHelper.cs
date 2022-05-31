using lijohnttle.Media.Photo.Core;
using System;
using System.Threading.Tasks;

namespace lijohnttle.Media.Photo.Filters.Internal.Helpers
{
    internal static class ImageProcessingHelper
    {
        /// <summary>
        /// Iterates image pixels.
        /// </summary>
        /// <param name="image">The original image that is being processed.</param>
        /// <param name="action">The action to process pixel.</param>
        public static void IterateImagePixels(this IImage image, Action<int, int> action,
            ImagePixelsIterationMode mode = ImagePixelsIterationMode.InParallel)
        {
            if (mode == ImagePixelsIterationMode.Sequential)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        action(x, y);
                    }
                }
            }
            else
            {
                Parallel.For(0, image.Height - 1, y =>
                {
                    if (mode == ImagePixelsIterationMode.InParallel)
                    {
                        Parallel.For(0, image.Width - 1, x => action(x, y));
                    }
                    else
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            action(x, y);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Iterates image pixels within a processing matrix.
        /// </summary>
        /// <param name="image">The original image that is being processed.</param>
        /// <param name="x">The X index of the current pixel.</param>
        /// <param name="y">The Y index of the current pixel.</param>
        /// <param name="radius">The half-length of the processing matrix.</param>
        /// <param name="action">The action to process pixel.</param>
        /// <param name="rowByRow">Determines if iterations should be done row-by-row or column-by-column.</param>
        public static void IterateMatrix(this IImage image, int x, int y, int radius, Action<int, int> action,
            bool rowByRow = true)
        {
            if (rowByRow)
            {
                for (int windowY = Math.Max(0, y - radius); windowY <= Math.Min(image.Height - 1, y + radius); windowY++)
                {
                    for (int windowX = Math.Max(0, x - radius); windowX <= Math.Min(image.Width - 1, x + radius); windowX++)
                    {
                        action(windowX, windowY);
                    }
                }
            }
            else
            {
                for (int windowX = Math.Max(0, x - radius); windowX <= Math.Min(image.Width - 1, x + radius); windowX++)
                {
                    for (int windowY = Math.Max(0, y - radius); windowY <= Math.Min(image.Height - 1, y + radius); windowY++)
                    {
                        action(windowX, windowY);
                    }
                }
            }
        }
    }
}
