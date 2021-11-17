﻿using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace IconsApp.Services
{
    public class SavePngService : ISaveImageService
    {
        public bool Save(string path, BitmapSource bitmapSource)
        {
            try
            {
                using var stream = new FileStream(path, FileMode.Create);
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
