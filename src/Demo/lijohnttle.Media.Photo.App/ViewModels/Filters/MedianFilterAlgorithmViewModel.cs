using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.Filters.Median;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public class MedianFilterAlgorithmViewModel : ViewModelBase
    {
        public MedianFilterAlgorithmViewModel(string title, IMedianFilterAlgorithm algorithm)
        {
            Title = title;
            Algorithm = algorithm;
        }

        public string Title { get; }

        public IMedianFilterAlgorithm Algorithm { get; }
    }
}
