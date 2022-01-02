using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uBeac.Identity;
using uBeac.Web.Identity;

namespace Identity
{
    public class AccountController : AccountControllerBase<User>
    {
        public AccountController(IUserService<User> userService) : base(userService)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public string GetAll() 
        {
            return "Teeee";
        }

    }
}

