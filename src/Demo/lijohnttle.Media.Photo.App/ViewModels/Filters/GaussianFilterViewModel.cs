using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.Filters;
using lijohnttle.Media.Photo.Filters.Gaussian;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public class GaussianFilterViewModel : FilterViewModel
    {
        public GaussianFilterViewModel(IMessenger messenger)
            : base(messenger)
        {
            Radius = 1;
        }


        public override string Title { get; } = "Gaussian Filter";

        public int Radius
        {
            get => GetPropertyValue<int>(nameof(Radius));
            set => SetPropertyValue(nameof(Radius), value);
        }


        public override IImageFilter BuildFilter()
        {
            return new GaussianFilter
            {
                Options = new GaussianFilterOptions
                {
                    Radius = Radius
                }
            };
        }
    }
}
