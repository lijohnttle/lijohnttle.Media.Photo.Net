using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.Filters;
using lijohnttle.Media.Photo.Filters.Median;
using lijohnttle.Media.Photo.Filters.Median.Algorithms;
using System.Collections.ObjectModel;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public class MedianFilterViewModel : FilterViewModel
    {
        public MedianFilterViewModel(IMessenger messenger)
            : base(messenger)
        {
            Radius = 3;
            Algorithms = new ObservableCollection<MedianFilterAlgorithmViewModel>
            {
                new MedianFilterAlgorithmViewModel("Default", StandardMedianFilterAlgorithm.Default),
                new MedianFilterAlgorithmViewModel("Huang", HuangMedianFilterAlgorithm.Default),
            };
            Algorithm = Algorithms[0];
        }


        public override string Title { get; } = "Median Filter";

        public int Radius
        {
            get => GetPropertyValue<int>(nameof(Radius));
            set => SetPropertyValue(nameof(Radius), value);
        }

        public MedianFilterAlgorithmViewModel Algorithm
        {
            get => GetPropertyValue<MedianFilterAlgorithmViewModel>(nameof(Algorithm));
            set => SetPropertyValue<MedianFilterAlgorithmViewModel>(nameof(Algorithm), value);
        }

        public ObservableCollection<MedianFilterAlgorithmViewModel> Algorithms { get; }


        public override IImageFilter BuildFilter()
        {
            return new MedianFilter
            {
                Options = new MedianFilterOptions
                {
                    Radius = Radius
                },
                Algorithm = Algorithm.Algorithm,
            };
        }
    }
}
