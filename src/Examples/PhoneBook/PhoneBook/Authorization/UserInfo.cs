using System.Security.Claims;

namespace PhoneBook;

public interface IUserInfo
{
    string Id { get; }
}

public class UserInfo : IUserInfo
{
    public UserInfo(IHttpContextAccessor accessor)
    {
        Id = accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string Id { get; }
}