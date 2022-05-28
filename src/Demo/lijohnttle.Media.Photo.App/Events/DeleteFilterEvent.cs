using lijohnttle.Media.Photo.App.ViewModels.Filters;

namespace lijohnttle.Media.Photo.App.Events
{
    public class DeleteFilterEvent
    {
        public DeleteFilterEvent(IFilterViewModel filter)
        {
            Filter = filter;
        }

        public IFilterViewModel Filter { get; }
    }
}
