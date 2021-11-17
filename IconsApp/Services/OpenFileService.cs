using Microsoft.Win32;

namespace IconsApp.Services
{
    public class OpenFileService : IOpenFileService
    {
        public string Filter { get; set; }
        public string OpenFile()
        {
            var openFile = new OpenFileDialog();
            openFile.Filter = Filter;
            openFile.ShowDialog();
            return openFile.FileName;
        }
    }
}
