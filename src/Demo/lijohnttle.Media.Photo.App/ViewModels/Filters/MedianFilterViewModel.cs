using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.Filters;
using lijohnttle.Media.Photo.Filters.Median;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public class MedianFilterViewModel : FilterViewModel
    {
        public MedianFilterViewModel(IMessenger messenger)
            : base(messenger)
        {
            Radius = 3;
        }

        public override string Title { get; } = "Median Filter";

        public int Radius
        {
            get => GetPropertyValue<int>(nameof(Radius));
            set => SetPropertyValue(nameof(Radius), value);
        }

        public override IImageFilter BuildFilter()
        {
            return new MedianFilter
            {
                Options = new MedianFilterOptions
                {
                    Radius = Radius
                }
            };
        }
    }
}
