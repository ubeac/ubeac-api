using Identity.EntityFramework.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;

namespace Identity.EntityFramework.API.Services
{
    public interface IAppUserService : IUserService<User>
    {
        Task<List<UserRoles>> GetAllUsersWithRoles(CancellationToken cancellationToken = default);
    }
    public class AppUserService : UserService<User>, IAppUserService
    {
        private readonly UserManager<User> _userManager;
        public AppUserService(UserManager<User> userManager, ITokenService<User> tokenService, IUserTokenRepository userTokenRepository, IEmailProvider emailProvider, IApplicationContext appContext, IHttpContextAccessor accessor) : base(userManager, tokenService, userTokenRepository, emailProvider, appContext, accessor)
        {
            _userManager = userManager;
        }

        public async Task<List<UserRoles>> GetAllUsersWithRoles(CancellationToken cancellationToken = default)
        {
            return _userManager.Users.Include(x => x.Roles).Select(x => new UserRoles
            {
                Email = x.Email,
                UserName = x.UserName,
                Roles = x.Roles
            }).ToList();
        }
    }
}
