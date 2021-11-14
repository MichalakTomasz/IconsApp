using IconsApp.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace IconsApp
{
    public class SaveFileService : ISaveFileService
    {
        public string SaveFile(string path)
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = Literals.ExtFilter;
            saveFile.ShowDialog();
            return saveFile.FileName;
        }
    }
}
