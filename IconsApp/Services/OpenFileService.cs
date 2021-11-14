using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace IconsApp.Services
{
    public class OpenFileService : IOpenFileService
    {
        public string OpenFile()
        {
            var openFile = new OpenFileDialog();
            openFile.Filter = Literals.ExtFilter;
            openFile.ShowDialog();
            return openFile.FileName;
        }
    }
}
