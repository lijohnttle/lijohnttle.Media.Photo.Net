using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.Core;
using System.Windows.Input;

namespace lijohnttle.Media.Photo.App.ViewModels.Filters
{
    public abstract class FilterViewModel : ViewModelBase, IFilterViewModel
    {
        protected FilterViewModel(IMessenger messenger)
        {
            Messenger = messenger;
            DeleteCommand = new DelegateCommand(() => messenger.Publish(new DeleteFilterEvent(this)));
        }

        public ICommand DeleteCommand { get; }

        public abstract string Title { get; }

        public IMessenger Messenger { get; }

        public abstract IImage ApplyFilter(IImage image);
    }
}
