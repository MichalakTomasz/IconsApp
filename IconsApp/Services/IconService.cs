using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace IconsApp.Services
{
    public class IconService : IIconService
    {
        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
        [System.Runtime.InteropServices.DllImport("shell32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int ExtractIconEx(string stExeFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        public IEnumerable<BitmapSource> GetIcons(string path)
        {
            var iconsCount = ExtractIconEx(path, 0, null, null, 0);
            var lIcons = new IntPtr[iconsCount];
            ExtractIconEx(path, 0, lIcons, null, iconsCount);
            return lIcons.ToList().Select(lIcon => Imaging.CreateBitmapSourceFromHIcon(
                lIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())).ToList();
        }

        public BitmapSource GetAssociatedIcon(string path)
        {
            var icon = Icon.ExtractAssociatedIcon(path);
            var intptr = icon.Handle;
            var bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                intptr, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return bitmapSource;
        }
    }
}
