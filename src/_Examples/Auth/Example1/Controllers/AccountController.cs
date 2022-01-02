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

        [HttpGet]
        [AllowAnonymous]
        public async Task Test()
        {
            var userName = "admin";
            var email = "ap@momentaj.com";
            var password = "zxcASD123!@#";

            await Register(new RegisterRequest { Email = email, Password = password, Username = userName });

            var tokenResult = await Login(new LoginRequest { Username = userName, Password = password });

        }
    }
}
