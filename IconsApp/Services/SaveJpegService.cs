using System;
using System.Windows.Media.Imaging;

namespace IconsApp.Services
{
    public class SaveJpegService : ISaveImageService
    {
        public bool Save(string path, BitmapSource bitmapSource)
        {
            try
            {
                var encoder = new SaveJpegService();
                encoder.Save(path, bitmapSource);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
