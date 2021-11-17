namespace IconsApp
{
    public interface ISaveImageService
    {
        string SaveFile();
        string SelectedExtension { get; }
    }
}