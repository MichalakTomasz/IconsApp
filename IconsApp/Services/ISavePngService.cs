using System.Windows.Media.Imaging;

namespace IconsApp.Services
{
    public interface ISavePngService
    {
        bool Save(string path, BitmapSource bitmapSource);
    }
}