namespace uBeac.FileManagement;

public class FileCategory
{
    public string CategoryName { get; set; }
    public IFileService Service { get; set; }
}