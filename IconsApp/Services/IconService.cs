using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace IconsApp.Services
{
    public class IconService
    {
        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
        [System.Runtime.InteropServices.DllImport("shell32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int ExtractIconEx(string stExeFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
        public object GetAssociatedIcon(string path)
        => Icon.ExtractAssociatedIcon(path);

        public IEnumerable<(int nr, BitmapSource image)> GetIcons(string path)
        {
            var iconsCount = ExtractIconEx(path, 0, null, null, 0);
            var images = new List<(int, BitmapSource)>();
            Enumerable.Range(0, iconsCount).ToList().ForEach(i =>
            {
                var licon = new IntPtr[1];
                ExtractIconEx(path, i, licon, null, 1);
                var icon = Icon.FromHandle(licon[0]);
                var imageSource = Imaging.CreateBitmapSourceFromHIcon(licon[0], new System.Windows.Int32Rect(), BitmapSizeOptions.FromEmptyOptions());
                DestroyIcon(licon[0]);
                images.Add((i, imageSource));
            });

            return images;
        }

    }
}
