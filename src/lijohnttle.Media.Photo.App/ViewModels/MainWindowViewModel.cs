using lijohnttle.Media.Photo.App.Commands;
using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace lijohnttle.Media.Photo.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IMessenger messenger)
        {
            SelectImageCommand = new SelectImageCommand(messenger);

            messenger.Subscribe<ImageFileSelectedEvent>(OnImageFileSelected);
        }

        public ICommand SelectImageCommand { get; }

        public string ImageFileName
        {
            get => GetPropertyValue<string>(nameof(ImageFileName));
            set
            {
                if (SetPropertyValue(nameof(ImageFileName), value))
                {
                    LoadImage(ImageFileName);
                }
            }
        }

        public ImageSource Image
        {
            get => GetPropertyValue<ImageSource>(nameof(Image));
            private set => SetPropertyValue(nameof(Image), value);
        }

        private void LoadImage(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Image = null;
            }

            BitmapSource image = new BitmapImage(new System.Uri(fileName));

            Image = image;
        }

        private void OnImageFileSelected(ImageFileSelectedEvent message)
        {
            ImageFileName = message.FileName;
        }
    }
}
