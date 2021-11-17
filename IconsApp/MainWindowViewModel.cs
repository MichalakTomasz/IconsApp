using IconsApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace IconsApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IconService _iconService;
        private readonly OpenFileService _openFileService;
        private SaveFileService _saveFileService;
        private SavePngService _savePngService;
        private SaveJpegService _saveJpegService;
        private SaveBmpService _saveBmpService;

        public MainWindowViewModel()
        {
            _iconService = new IconService();
            _openFileService = new OpenFileService();
            _saveFileService = new SaveFileService();
            _savePngService = new SavePngService();
            _saveJpegService = new SaveJpegService();
            _saveBmpService = new SaveBmpService();
            LoadIconsCommand = new CommandHelper(e =>
            {
                var path = _openFileService.OpenFile();
                if (string.IsNullOrEmpty(path))
                    return;
                var images = _iconService.GetIcons(path);
                images.ToList().ForEach(image => ImageSources.Add(image));
            }, e => true);

            SaveCommand = new CommandHelper(e =>
            {
                var path = _saveFileService.SaveFile();
                var ext = _saveFileService.SelectedExtension;
                SaveFile(path, ext);
            }, e => true);

            SaveAssociatedIconCommand = new CommandHelper(() =>
            {
                var iconPath = _openFileService.OpenFile();
                if (string.IsNullOrEmpty(iconPath))
                    return;
                _saveFileService.Filter = Literals.ExtFilter;
                var destinationPath  = _saveFileService.SaveFile();
                var ext = _saveFileService.SelectedExtension;
                var bitmapSource = _iconService.GetAssociatedIcon(iconPath);
                SaveFile(destinationPath, ext);
            });
        }

        private void SaveFile(string path, string ext)
        {
            switch (ext.ToLower())
            {
                case Literals.jpg:
                    _saveJpegService.Save(path, SelectedItem);
                    break;
                case Literals.jpeg:
                    _saveJpegService.Save(path, SelectedItem);
                    break;
                case Literals.png:
                    _savePngService.Save(path, SelectedItem);
                    break;
                case Literals.bmp:
                    _saveBmpService.Save(path, SelectedItem);
                    break;
            }
        }

        public ICommand LoadIconsCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAssociatedIconCommand { get; }

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
