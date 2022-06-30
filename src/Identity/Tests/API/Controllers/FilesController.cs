using Microsoft.AspNetCore.Mvc;
using uBeac.FileManagement;
using uBeac.Web;

namespace API;

public class FilesController : BaseController
{
    protected readonly IFileManager FileManager;

    public FilesController(IFileManager fileManager)
    {
        FileManager = fileManager;
    }

    [HttpPost]
    public async Task Avatar([FromForm] IFormFile file, CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();
        await FileManager.Create(new CreateFileRequest
        {
            Stream = stream,
            Category = "Avatars",
            Extension = Path.GetExtension(file.FileName)
        }, cancellationToken);
    }

    [HttpPost]
    public async Task Document([FromForm] IFormFile file, CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();
        await FileManager.Create(new CreateFileRequest
        {
            Stream = stream,
            Category = "Documents",
            Extension = Path.GetExtension(file.FileName)
        }, cancellationToken);
    }
}