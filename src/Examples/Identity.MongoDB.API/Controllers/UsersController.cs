using AutoMapper;

namespace API;

public class UsersController : UsersControllerBase<User>
{
    public UsersController(IUserService<User> userService, IMapper mapper) : base(userService, mapper)
    {
    }
}