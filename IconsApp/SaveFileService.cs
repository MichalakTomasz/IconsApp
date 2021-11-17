﻿using Microsoft.Win32;
using System.IO;

namespace IconsApp
{
    public class SaveFileService : ISaveImageService
    {
        private string _ext;

        public string SaveFile()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = Literals.ExtFilter;
            saveFile.ShowDialog();
            _ext = Path.GetExtension(saveFile.FileName);
            return saveFile.FileName;
        }

        public string SelectedExtension => _ext;
    }
}
