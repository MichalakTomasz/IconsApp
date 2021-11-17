using Microsoft.Win32;
using System.IO;

namespace IconsApp.Services
{
    public class OpenFileService : IOpenFileService
    {
        public string Filter { get; set; }
        public string Filename { get; internal set; }

        public string OpenFile()
        {
            var openFile = new OpenFileDialog();
            openFile.Filter = Filter;
            openFile.ShowDialog();
            Filename = Path.GetFileName(openFile.FileName);
            return openFile.FileName;
        }
    }
}
