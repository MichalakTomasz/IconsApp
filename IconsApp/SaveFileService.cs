using Microsoft.Win32;

namespace IconsApp
{
    public class SaveFileService : ISaveFileService
    {
        public string Filter { get; set; }
        public string SaveFile()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = Filter;
            saveFile.ShowDialog();
            return saveFile.FileName;
        }
    }
}
