namespace uBeac.FileManagement;

public class FileExtensionValidator : IFileValidator
{
    protected readonly string[] ValidExtensions;

    public FileExtensionValidator(IEnumerable<string> validExtensions)
    {
        ValidExtensions = validExtensions.Select(e => e.ToUpper()).ToArray();
    }

    public IFileValidationResult Validate(FileStream fileStream)
    {
        var extension = Path.GetExtension(fileStream.Name).ToUpper();
        return ValidExtensions.Contains(extension) ? new FileValidationResult() : new FileValidationResult(new Exception("File extension is not valid!"));
    }
}