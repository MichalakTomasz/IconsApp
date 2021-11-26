namespace IconsApp
{
    public interface ISaveFileService
    {
        string Filter { get; set; }
        string SaveFile();
        string SelectedExtension { get; }
    }
}