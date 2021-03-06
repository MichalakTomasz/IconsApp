using Microsoft.Win32;
using System.IO;

namespace IconsApp.Services
{
    public class SaveFileService : ISaveFileService
    {
        private string _ext;
        public string Filter { get; set; }
        public string SaveFile()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = Filter;
            saveFile.ShowDialog();
            _ext = Path.GetExtension(saveFile.FileName);
            return saveFile.FileName;
        }

        public string SelectedExtension => _ext;
    }
}
