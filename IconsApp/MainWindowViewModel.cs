using IconsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace IconsApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IconService _iconService;
        private readonly OpenFileService _openFileService;
        private SaveFileService _saveFileService;
        private SavePngService _savePngService;

        public MainWindowViewModel()
        {
            _iconService = new IconService();
            _openFileService = new OpenFileService();
            _saveFileService = new SaveFileService();
            _savePngService = new SavePngService();
            LoadIconsCommand = new CommandHelper(e =>
            {
                var path = _openFileService.OpenFile();
                var images = _iconService.GetIcons(path);
                images.ToList().ForEach(image =>
                {
                    ImageSources.Add(image);
                });
            }, e => true);

            SaveCommand = new CommandHelper(e =>
            {
                var path = _saveFileService.SaveFile();
                _savePngService.Save(path, SelectedItem);
            }, e => true);
        }

        public ICommand LoadIconsCommand { get; }
        public ICommand SaveCommand { get; }

        private ObservableCollection<BitmapSource> _imageSource = 
            new ObservableCollection<BitmapSource>();
        public ObservableCollection<BitmapSource> ImageSources
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        private BitmapSource _selectedItem;
        public BitmapSource SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
