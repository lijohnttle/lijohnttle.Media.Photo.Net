namespace lijohnttle.Media.Photo.Filters.Cartoon
{
    public class CartoonFilterOptions
    {
        public CartoonFilterOptions()
        {
            SmoothingFilterType = CartoonSmoothingFilterType.Median3x3;
            Threshold = 80;
        }

        public CartoonSmoothingFilterType SmoothingFilterType { get; init; }

        public byte Threshold { get; init; }
    }
}
