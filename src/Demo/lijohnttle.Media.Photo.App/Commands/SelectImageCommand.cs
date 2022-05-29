using lijohnttle.Media.Photo.App.Events;
using Microsoft.Win32;

namespace lijohnttle.Media.Photo.App.Commands
{
    public class SelectImageCommand : CommandBase
    {
        private readonly IMessenger messenger;

        public SelectImageCommand(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public override void Execute(object parameter)
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Select image...",
                Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
            };

            var result = dialog.ShowDialog();

            if (result == true)
            {
                messenger.Publish(new ImageFileSelectedEvent(dialog.FileName));
            }
        }
    }
}
