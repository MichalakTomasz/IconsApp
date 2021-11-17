namespace IconsApp.Services
{
    public interface IOpenFileService
    {
        string Filter { get; set; }
        string OpenFile();
    }
}