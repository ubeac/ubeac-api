using uBeac.FileManagement;

namespace API;

public class AccountsController : AccountsControllerBase<User>
{
    public AccountsController(IUserService<User> userService, IFileManager fileManager) : base(userService)
    {
        using var avatar = File.OpenRead("C:\\Users\\Hesam\\Pictures\\f518a14c0cad2394cefc700675bb7b4d.jpg");
        fileManager.Create(avatar, "Avatars").Wait();

        using var document = File.OpenRead("C:\\Users\\Hesam\\Pictures\\ds.png");
        fileManager.Create(document, "Documents").Wait();
    }
}