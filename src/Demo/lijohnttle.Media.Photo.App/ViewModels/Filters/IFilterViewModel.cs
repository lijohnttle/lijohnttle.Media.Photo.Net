using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Filters;
using System.ComponentModel;
using System.Windows.Input;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public interface IFilterViewModel : INotifyPropertyChanged
    {
        ICommand DeleteCommand { get; }

        string Title { get; }

        IImageFilter BuildFilter();
    }
}
