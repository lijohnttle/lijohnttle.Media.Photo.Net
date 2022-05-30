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
        public static void IteratePixelsInParallel(this IImage image, Action<int, int> action)
        {
            Parallel.For(0, image.Height - 1, y =>
            {
                Parallel.For(0, image.Width - 1, x => action(x, y));
            });
        }

        /// <summary>
        /// Iterates image pixels.
        /// </summary>
        /// <param name="image">The original image that is being processed.</param>
        /// <param name="action">The action to process pixel.</param>
        public static void IteratePixels(this IImage image, Action<int, int> action)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    action(x, y);
                }
            }
        }

        /// <summary>
        /// Iterates image pixels within a processing matrix.
        /// </summary>
        /// <param name="image">The original image that is being processed.</param>
        /// <param name="x">The X index of the current pixel.</param>
        /// <param name="y">The Y index of the current pixel.</param>
        /// <param name="filterOffset">The half-length of the processing matrix.</param>
        /// <param name="action">The action to process pixel.</param>
        public static void IterateMatrixPixels(this IImage image, int x, int y, int filterOffset, Action<int, int> action)
        {
            for (int windowY = Math.Max(0, y - filterOffset); windowY <= Math.Min(image.Height - 1, y + filterOffset); windowY++)
            {
                for (int windowX = Math.Max(0, x - filterOffset); windowX <= Math.Min(image.Width - 1, x + filterOffset); windowX++)
                {
                    action(windowX, windowY);
                }
            }
        }
    }
}
