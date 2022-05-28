using lijohnttle.Media.Photo.App.Commands;
using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.App.ViewModels.Filters;
using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Wpf.Extensions;
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

            _originalImage = bitmapSource.ConvertToImage();

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

            IImage image = _originalImage;

            foreach (IFilterViewModel filterViewModel in Filters)
            {
                image = filterViewModel.ApplyFilter(_originalImage);
            }

            Image = image.ConvertToBitmapSource();
        }
    }
}
