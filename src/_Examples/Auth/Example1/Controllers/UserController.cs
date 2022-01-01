using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers
{
    public class UserController : UserControllerBase<User>
    {
        private readonly IRoleService<Role> RoleService;
        private readonly IUserRoleService<User> UserRoleService;
        public UserController(IUserService<User> userService, IRoleService<Role> roleService, IUserRoleService<User> userRoleService) : base(userService)
        {
            RoleService = roleService;
            UserRoleService = userRoleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task Test()
        {
            var userName = "admin12";
            var email = "ap1@momentaj.com";
            var password = "zxcASD123!@#";

            var user = await UserService.Register(userName, email, password);

            var roleName = "Role1";
            
            if (!await RoleService.Exists(roleName))
                await RoleService.Insert(new Role { Name = roleName });

            await UserRoleService.AddRoles(user.Id, new[] { roleName });


        }
    }
}
