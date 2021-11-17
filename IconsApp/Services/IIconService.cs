using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace IconsApp.Services
{
    public interface IIconService
    {
        BitmapSource GetAssociatedIcon(string path);
        IEnumerable<BitmapSource> GetIcons(string path);
    }
}