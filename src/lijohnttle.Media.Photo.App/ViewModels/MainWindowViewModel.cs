using lijohnttle.Media.Photo.App.Commands;
using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.App.ViewModels.Filters;
using lijohnttle.Media.Photo.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace lijohnttle.Media.Photo.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IImage _originalImage;


        public MainWindowViewModel(IMessenger messenger)
        {
            SelectImageCommand = new SelectImageCommand(messenger);
            AddMedianFilterCommand = new DelegateCommand(AddMedianFilter);
            RenderCommand = new DelegateCommand(Render);
            Filters = new ObservableCollection<IFilterViewModel>();

            messenger.Subscribe<ImageFileSelectedEvent>(OnImageFileSelected);
        }


        public ICommand SelectImageCommand { get; }

        public ICommand AddMedianFilterCommand { get; }

        public ICommand RenderCommand { get; }

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

        public ObservableCollection<IFilterViewModel> Filters { get; }


        private void LoadImage(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Image = null;
            }

            BitmapSource bitmapSource = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(fileName));

            _originalImage = ImageFromBitmapSource(bitmapSource);

            Render();
        }

        private void OnImageFileSelected(ImageFileSelectedEvent message)
        {
            ImageFileName = message.FileName;
        }

        private void AddMedianFilter()
        {
            Filters.Add(new MedianFilterViewModel());
        }

        private void Render()
        {
            if (_originalImage == null)
            {
                Image = null;
                return;
            }

            Image = ImageToBitmapSource(_originalImage);
        }

        private IImage ImageFromBitmapSource(BitmapSource bitmapSource)
        {
            int bytesPerPixel = (bitmapSource.Format.BitsPerPixel + 7) / 8;

            RgbColor[,] data = new RgbColor[bitmapSource.PixelWidth, bitmapSource.PixelHeight];

            for (int x = 0; x < bitmapSource.PixelWidth; x++)
            {
                for (int y = 0; y < bitmapSource.PixelHeight; y++)
                {
                    byte[] pixel = new byte[bytesPerPixel];
                    bitmapSource.CopyPixels(new Int32Rect(x, y, 1, 1), pixel, bytesPerPixel, 0);
                    data[x, y] = new RgbColor(pixel[2], pixel[1], pixel[0], pixel.Length == 4 ? (float)pixel[3] / 255 : 1);
                }
            }

            return new Core.BitmapImage(data);
        }

        private BitmapSource ImageToBitmapSource(IImage image)
        {
            if (image == null)
            {
                return null;
            }

            foreach (IFilterViewModel filterViewModel in Filters)
            {
                image = filterViewModel.ApplyFilter(_originalImage);
            }

            var stride = image.Width * 4;
            byte[] array = new byte[stride * image.Height];
            CopyPixels(image, array);

            var bitmapSource = BitmapSource.Create(
                image.Width,
                image.Height,
                96,
                96,
                PixelFormats.Bgra32,
                null,
                array,
                stride);

            return bitmapSource;
        }

        public void CopyPixels(IImage image, byte[] array)
        {
            int index = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var color = image.GetPixelColor(x, y).AsRgbColor();

                    array[index] = color.Blue;
                    array[index + 1] = color.Green;
                    array[index + 2] = color.Red;
                    array[index + 3] = (byte)(color.Alpha * 255);

                    index += 4;
                }
            }
        }
    }
}
