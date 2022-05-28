using lijohnttle.Media.Photo.Core;
using System.ComponentModel;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public interface IFilterViewModel : INotifyPropertyChanged
    {
        string Title { get; }

        IImage ApplyFilter(IImage image);
    }
}
