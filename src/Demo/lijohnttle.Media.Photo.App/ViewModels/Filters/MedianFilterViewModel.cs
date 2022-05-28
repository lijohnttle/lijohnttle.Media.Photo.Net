using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters.Extensions;
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
            set => SetPropertyValue(nameof(WindowSize), value);
        }

        public override IImage ApplyFilter(IImage image)
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
