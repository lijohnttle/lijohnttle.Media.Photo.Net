using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Extensions;
using lijohnttle.Media.Photo.Filters.Median;
using System.Windows.Input;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public class MedianFilterViewModel : ViewModelBase, IFilterViewModel
    {
        public MedianFilterViewModel()
        {
            WindowSize = 3;

            IncreaseWindowSizeCommand = new DelegateCommand(IncreaseWindowSize);
            DecreaseWindowSizeCommand = new DelegateCommand(DecreaseWindowSize, CanDecreaseWindowSize);
        }

        public ICommand IncreaseWindowSizeCommand { get; }

        public ICommand DecreaseWindowSizeCommand { get; }

        public string Title => "Median Filter";

        public int WindowSize
        {
            get => GetPropertyValue<int>(nameof(WindowSize));
            set => SetPropertyValue(nameof(WindowSize), value);
        }

        public IImage ApplyFilter(IImage image)
        {
            return image.ApplyMedianFilter(new MedianFilterOptions
            {
                WindowSize = WindowSize
            });
        }

        private bool CanDecreaseWindowSize()
        {
            return WindowSize > 3;
        }

        private void DecreaseWindowSize()
        {
            WindowSize -= 2;
        }

        private void IncreaseWindowSize()
        {
            WindowSize += 2;
        }
    }
}
