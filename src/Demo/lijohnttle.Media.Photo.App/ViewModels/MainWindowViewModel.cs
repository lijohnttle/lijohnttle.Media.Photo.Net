using lijohnttle.Media.Photo.App.Commands;
using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.Models;
using lijohnttle.Media.Photo.App.ViewModels.Common;
using lijohnttle.Media.Photo.App.ViewModels.Filters;
using lijohnttle.Media.Photo.Core;
using lijohnttle.Media.Photo.Wpf.Extensions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace lijohnttle.Media.Photo.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IMessenger messenger;
        private readonly IImageProcessor imageProcessor;
        private IImage originalImage;


        public MainWindowViewModel(IImageProcessor imageProcessor, IMessenger messenger)
        {
            SelectImageCommand = new SelectImageCommand(messenger);
            RenderCommand = new DelegateCommand(Render, CanRender);
            this.imageProcessor = imageProcessor;
            this.messenger = messenger;
            FiltersList = new FiltersListViewModel(messenger);

            messenger.Subscribe<ImageFileSelectedEvent>(OnImageFileSelected);
        }

        
        public ICommand SelectImageCommand { get; }

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

        public bool IsProcessing
        {
            get => GetPropertyValue<bool>(nameof(IsProcessing));
            set => SetPropertyValue(nameof(IsProcessing), value);
        }

        public FiltersListViewModel FiltersList { get; }


        private void LoadImage(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Image = null;
            }

            BitmapSource bitmapSource = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(fileName));
            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapSource);

            originalImage = writeableBitmap.ConvertToImage();

            Render();
        }

        private void OnImageFileSelected(ImageFileSelectedEvent message)
        {
            ImageFileName = message.FileName;
        }

        private async void Render()
        {
            if (originalImage == null)
            {
                Image = null;
                return;
            }

            IsProcessing = true;

            IImage image = await imageProcessor.ProcessImageAsync(originalImage, FiltersList.BuildFilters());

            Image = image.ConvertToBitmapSource();

            IsProcessing = false;

            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanRender() => !IsProcessing;
    }
}
