using AutoMapper;
using Identity.EntityFramework.API.Services;
using Identity.EntityFramework.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class UsersController : UsersControllerBase<User>
{
    private readonly IAppUserService _userService;

    public UsersController(IAppUserService userService, IMapper mapper) : base(userService, mapper)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<UserRoles>> GetAllUsersWithRoles(CancellationToken cancellationToken = default)
    {
        return await _userService.GetAllUsersWithRoles(cancellationToken);
    }
}