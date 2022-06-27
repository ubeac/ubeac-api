namespace uBeac.FileManagement;

public interface IFileValidator
{
    IFileValidationResult Validate(FileStream fileStream);
}

public interface IFileValidationResult
{
    bool Validated { get; set; }
    Exception Exception { get; set; }
}

public class FileValidationResult : IFileValidationResult
{
    public FileValidationResult()
    {
        Validated = true;
        Exception = null;
    }

    public FileValidationResult(Exception exception)
    {
        Validated = false;
        Exception = exception;
    }

    public bool Validated { get; set; }
    public Exception Exception { get; set; }
}