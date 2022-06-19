using lijohnttle.Media.Photo.Core;
using System.Collections.Generic;

namespace lijohnttle.Media.Photo.Filters.Median
{
    public class MedianFilterPixelComparer : IComparer<RgbColor>
    {
        public static MedianFilterPixelComparer Default { get; } = new MedianFilterPixelComparer();

        public int Compare(RgbColor x, RgbColor y)
        {
            var xRgb = x.AsRgbColor();
            var yRgb = y.AsRgbColor();

            int result = CompareComponent(xRgb.Red, yRgb.Red);

            if (result != 0)
            {
                return result;
            }

            result = CompareComponent(xRgb.Green, yRgb.Green);

            if (result != 0)
            {
                return result;
            }

            result = CompareComponent(xRgb.Blue, yRgb.Blue);

            return result;
        }

        private int CompareComponent(int xComponent, int yComponent)
        {
            if (xComponent > yComponent)
            {
                return 1;
            }

            if (xComponent < yComponent)
            {
                return -1;
            }

            return 0;
        }
    }
}
