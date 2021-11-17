namespace IconsApp
{
    public interface ISaveImageService
    {
        string Filter { get; set; }
        string SaveFile();
        string SelectedExtension { get; }
    }
}