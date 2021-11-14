using IconsApp.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IconsApp
{
    public partial class MainWindow : Window
    {
        private string _path;
        private IconService _iconService;
        private MenuItem _menuItem;

        public MainWindow()
        {
            InitializeComponent();

            _iconService = new IconService();
        }

        private void otworzButton_Click(object sender, RoutedEventArgs e)
        {
            var open = new OpenFileDialog();
            open.ShowDialog();
            _path = open.FileName;
            var images = _iconService.GetIcons(_path);


            images.ToList().ForEach(bitmapSource =>
            {
                var menuItem = new MenuItem { DataContext = bitmapSource.image, Header = "Zapisz" };
                menuItem.Click += MenuItem_Click;
                wrapperImages.Children.Add(
                    new Image
                    {
                        Source = bitmapSource.image,
                        Width = 64,
                        Height = 64,
                        DataContext = bitmapSource.nr,
                        ContextMenu = new ContextMenu { ItemsSource = new[] { menuItem } }
                    });
                menuItem.Tag = bitmapSource.image;
            });
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var img = (e.OriginalSource as MenuItem).Tag as BitmapSource;//
            var saveDialog = new SaveFileDialog();
            saveDialog.ShowDialog();
            using var stream = new FileStream(saveDialog.FileName, FileMode.Create);
            var encoder = new PngBitmapEncoder();
            var bitmapSource = img;
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(stream);
        }
    }
}
