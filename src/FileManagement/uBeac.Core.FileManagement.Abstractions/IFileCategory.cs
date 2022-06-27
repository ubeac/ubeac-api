namespace uBeac.FileManagement;

public interface IFileCategory
{
    string CategoryName { get; }
    IFileService Service { get; }
}

public class FileCategory : IFileCategory
{
    public string CategoryName { get; set; }
    public IFileService Service { get; set; }
}