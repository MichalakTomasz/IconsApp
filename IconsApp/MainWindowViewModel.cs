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
        private readonly SaveFileService _saveFileService;
        private readonly SavePngService _savePngService;

        public MainWindowViewModel()
        {
            _iconService = new IconService();
            _openFileService = new OpenFileService();
            _saveFileService = new SaveFileService();
            _savePngService = new SavePngService();
            LoadIconsCommand = new CommandHelper(e =>
            {
                _openFileService.Filter = Literals.ExtFilter;
                var path = _openFileService.OpenFile();
                if (string.IsNullOrEmpty(path))
                    return;
                var images = _iconService.GetIcons(path);
                images.ToList().ForEach(image =>
                {
                    ImageSources.Add(image);
                });
            }, e => true);

            SaveCommand = new CommandHelper(e =>
            {
                var path = _saveFileService.SaveFile();
                if (string.IsNullOrEmpty(path))
                    return;
                _saveFileService.Filter = Literals.ExtFilter;
                _savePngService.Save(path, SelectedItem);
            }, e => true);

            SaveAssociatedIconCommand = new CommandHelper(() =>
            {
                var iconPath = _openFileService.OpenFile();
                if (string.IsNullOrEmpty(iconPath))
                    return;
                var destinationPath  = _saveFileService.SaveFile();
                var bitmapSource = _iconService.GetAssociatedIcon(iconPath);
                _savePngService.Save(destinationPath, bitmapSource);
            });
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
