//using Microsoft.AspNetCore.Identity;
//namespace uBeac.Identity
//{
//    public class UserRoleService<TKey, TUser, TRole> : IUserRoleService<TKey, TUser, TRole>
//        where TKey : IEquatable<TKey>
//        where TUser : User<TKey>
//        where TRole : Role<TKey>
//    {
//        private readonly UserManager<TUser> _userManager;
//        private readonly RoleManager<TRole> _roleManager;

//        public UserRoleService(UserManager<TUser> userManager, RoleManager<TRole> roleManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        public async Task<bool> AddRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default)
//        {
//            cancellationToken.ThrowIfCancellationRequested();

//            var user = await _userManager.FindByIdAsync(userId.ToString());

//            var idResult = await _userManager.AddToRolesAsync(user, roleIds.Select(x => x.ToString()));

//            return true;
//        }

//        public async Task<IEnumerable<TRole>> GetRolesForUser(TKey userId, CancellationToken cancellationToken = default)
//        {
//            cancellationToken.ThrowIfCancellationRequested();

//            var user = await _userManager.FindByIdAsync(userId.ToString());

//            return _roleManager.Roles.Where(x => user.Roles.Contains(x.Id)).AsEnumerable();
//        }

//        public async Task<IEnumerable<TUser>> GetUsersInRole(TKey roleId, CancellationToken cancellationToken = default)
//        {
//            cancellationToken.ThrowIfCancellationRequested();

//            return await _userManager.GetUsersInRoleAsync(roleId.ToString());
//        }

//        public async Task<bool> RemoveRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default)
//        {
//            cancellationToken.ThrowIfCancellationRequested();

//            var user = await _userManager.FindByIdAsync(userId.ToString());

//            var idResult = await _userManager.RemoveFromRolesAsync(user, roleIds.Select(x => x.ToString()));

//            return true;
//        }
//    }
//}
