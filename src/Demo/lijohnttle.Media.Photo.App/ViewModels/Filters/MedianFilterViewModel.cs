using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.Filters;
using lijohnttle.Media.Photo.Filters.Median;
using System.Windows.Input;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public class MedianFilterViewModel : FilterViewModel
    {
        public MedianFilterViewModel(IMessenger messenger)
            : base(messenger)
        {
            WindowSize = 3;

            IncreaseWindowSizeCommand = new DelegateCommand(IncreaseWindowSize);
            DecreaseWindowSizeCommand = new DelegateCommand(DecreaseWindowSize, CanDecreaseWindowSize);
        }

        public ICommand IncreaseWindowSizeCommand { get; }

        public ICommand DecreaseWindowSizeCommand { get; }

        public override string Title { get; } = "Median Filter";

        public int WindowSize
        {
            get => GetPropertyValue<int>(nameof(WindowSize));
            private set => SetPropertyValue(nameof(WindowSize), value);
        }

        public override IImageFilter BuildFilter()
        {
            return new MedianFilter
            {
                Options = new MedianFilterOptions
                {
                    WindowSize = WindowSize
                }
            };
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
