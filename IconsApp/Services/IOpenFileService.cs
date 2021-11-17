namespace IconsApp.Services
{
    public interface IOpenFileService
    {
        string Filename { get; }
        string Filter { get; set; }
        string OpenFile();
    }
}