using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.App.ViewModels.Filters;
using lijohnttle.Media.Photo.Filters;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace lijohnttle.Media.Photo.App.ViewModels
{
    public class FiltersListViewModel : ViewModelBase
    {
        private readonly IMessenger messenger;


        public FiltersListViewModel(IMessenger messenger)
        {
            Filters = new ObservableCollection<IFilterViewModel>();
            AddMedianFilterCommand = new DelegateCommand(AddMedianFilter);
            AddCartoonFilterCommand = new DelegateCommand(AddCartoonFilter);
            this.messenger = messenger;

            messenger.Subscribe<DeleteFilterEvent>(OnDeleteFilter);
        }


        public ICommand AddMedianFilterCommand { get; }

        public ICommand AddCartoonFilterCommand { get; }

        public ObservableCollection<IFilterViewModel> Filters { get; }


        public IImageFilter[] BuildFilters() => Filters.Select(t => t.BuildFilter()).ToArray();

        private void OnDeleteFilter(DeleteFilterEvent message)
        {
            Filters.Remove(message.Filter);
        }

        private void AddMedianFilter()
        {
            Filters.Add(new MedianFilterViewModel(messenger));
        }

        private void AddCartoonFilter()
        {
            Filters.Add(new CartoonFilterViewModel(messenger));
        }
    }
}
