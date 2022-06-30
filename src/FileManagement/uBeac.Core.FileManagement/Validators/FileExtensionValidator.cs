namespace uBeac.FileManagement;

public class FileExtensionValidator : IFileValidator
{
    protected readonly string[] ValidExtensions;

    public FileExtensionValidator(IEnumerable<string> validExtensions)
    {
        ValidExtensions = validExtensions.Select(e => e.ToUpper()).ToArray();
    }

    public IFileValidationResult Validate(CreateFileRequest request)
    {
        return ValidExtensions.Contains(request.Extension.ToUpper()) ? new FileValidationResult() : new FileValidationResult(new Exception("File extension is not valid!"));
    }
}