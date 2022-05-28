using lijohnttle.Media.Photo.App.Events;
using lijohnttle.Media.Photo.App.Models;
using lijohnttle.Media.Photo.App.ViewModels;
using System.Windows;

namespace lijohnttle.Media.Photo.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel(new ImageProcessor(), new Messenger());
        }
    }
}
