using Microsoft.AspNetCore.Mvc;
using uBeac.FileManagement;
using uBeac.Web;

namespace API;

public class AvatarsController : BaseController
{
    protected readonly IFileManager FileManager;

    public AvatarsController(IFileManager fileManager)
    {
        FileManager = fileManager;
    }

    [HttpPost]
    public async Task Upload([FromForm] IFormFile file, CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();
        await FileManager.Create(new FileModel
        {
            Stream = stream,
            Category = "Avatars",
            Extension = Path.GetExtension(file.FileName)
        }, cancellationToken);
    }

    [HttpPost]
    public async Task<FileStreamResult> Download([FromBody] GetFileRequest request, CancellationToken cancellationToken = default)
    {
        var response = await FileManager.Get(request, cancellationToken);
        return new FileStreamResult(response.Stream, "application/octet-stream") { FileDownloadName = $"{request.Name}.{response.Extension}" };
    }

    [HttpPost]
    public async Task<IEnumerable<IFileEntity>> Search([FromBody] SearchFileRequest request, CancellationToken cancellationToken = default)
    {
        request.Category = "Avatars";
        return await FileManager.Search(request, cancellationToken);
    }
}