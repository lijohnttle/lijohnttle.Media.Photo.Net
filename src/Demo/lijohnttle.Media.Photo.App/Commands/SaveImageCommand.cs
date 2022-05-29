using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace lijohnttle.Media.Photo.App.Commands
{
    public class SaveImageCommand : CommandBase
    {
        private readonly Func<ImageSource> imageSelector;

        public SaveImageCommand(Func<ImageSource> imageSelector)
        {
            this.imageSelector = imageSelector;
        }

        public override bool CanExecute(object parameter)
        {
            return imageSelector() != null;
        }

        public override void Execute(object parameter)
        {
            var imageToSave = imageSelector();

            if (imageSelector == null)
            {
                return;
            }

            var dialog = new SaveFileDialog()
            {
                Title = "Save image...",
                Filter = "Images (*.jpg)|*.jpg",
            };

            var result = dialog.ShowDialog();

            if (result == true)
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imageToSave));
                using (var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(fs);
                }
            }
        }
    }
}
