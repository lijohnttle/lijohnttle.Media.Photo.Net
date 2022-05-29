namespace lijohnttle.Media.Photo.Filters.Cartoon
{
    public class CartoonFilterOptions
    {
        public CartoonFilterOptions()
        {
            SmoothingFilterType = CartoonSmoothingFilterType.Median3x3;
            Threshold = 80;
            OutlineStrength = 255;
        }

        public CartoonSmoothingFilterType SmoothingFilterType { get; init; }

        public byte Threshold { get; init; }

        public byte OutlineStrength { get; init; }
    }
}
