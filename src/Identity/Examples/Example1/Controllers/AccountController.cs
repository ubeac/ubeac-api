using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;
using uBeac.Web.Identity;

namespace Example1.Controllers
{
    public class AccountController : AccountControllerBase<User>
    {
        public AccountController(IUserService<User> userService) : base(userService)
        {
        }
    }
}
