using System.Windows.Media.Imaging;

namespace IconsApp.Services
{
    public interface ISaveImageService
    {
        bool Save(string path, BitmapSource bitmapSource);
    }
}