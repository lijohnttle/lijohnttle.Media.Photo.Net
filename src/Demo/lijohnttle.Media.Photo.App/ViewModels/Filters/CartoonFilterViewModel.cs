using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.Filters;
using lijohnttle.Media.Photo.Filters.Cartoon;
using System;
using System.Collections.ObjectModel;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public class CartoonFilterViewModel : FilterViewModel
    {
        public CartoonFilterViewModel(IMessenger messenger)
            : base(messenger)
        {
            Threshold = 80;
            SmoothingFilterTypeList = new ObservableCollection<CartoonSmoothingFilterType>(
                Enum.GetValues<CartoonSmoothingFilterType>());
            SmoothingFilterType = CartoonSmoothingFilterType.Median3x3;
        }

        public override string Title { get; } = "Cartoon Filter";

        public byte Threshold
        {
            get => GetPropertyValue<byte>(nameof(Threshold));
            set => SetPropertyValue(nameof(Threshold), value);
        }

        public CartoonSmoothingFilterType SmoothingFilterType
        {
            get => GetPropertyValue<CartoonSmoothingFilterType>(nameof(SmoothingFilterType));
            set => SetPropertyValue(nameof(SmoothingFilterType), value);
        }

        public ObservableCollection<CartoonSmoothingFilterType> SmoothingFilterTypeList { get; }

        public override IImageFilter BuildFilter()
        {
            return new CartoonFilter
            {
                Options = new CartoonFilterOptions
                {
                    Threshold = Threshold,
                    SmoothingFilterType = SmoothingFilterType,
                }
            };
        }
    }
}
