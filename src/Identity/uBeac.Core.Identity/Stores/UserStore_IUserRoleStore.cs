using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public partial class UserStore<TUser, TUserKey> : IUserRoleStore<TUser>
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {

        public Task AddToRoleAsync(TUser user, string roleId, CancellationToken cancellationToken)
        {
            user.Roles.Add(roleId);
            return Task.CompletedTask;
        }

        public Task RemoveFromRoleAsync(TUser user, string roleId, CancellationToken cancellationToken)
        {
            user.Roles.Remove(roleId);
            return Task.CompletedTask;
        }

        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.Find(x => x.Roles.Any(r => roleId.Equals(r)), cancellationToken)).ToList();
        }

        public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Roles);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleId, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Roles.Contains(roleId));
        }
    }
}
