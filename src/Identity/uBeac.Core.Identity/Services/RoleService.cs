using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public class RoleService<TKey, TRole> : IRoleService<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey>
    {

        private readonly RoleManager<TRole> _roleManager;

        public RoleService(RoleManager<TRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null) return false;

            var idResult = await _roleManager.DeleteAsync(role);

            idResult.ThrowIfInvalid();

            return true;
        }

        public virtual Task<IEnumerable<TRole>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(_roleManager.Roles.AsEnumerable());
        }

        public virtual async Task Insert(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var idResult = await _roleManager.CreateAsync(role);

            idResult.ThrowIfInvalid();

        }

        public virtual async Task<bool> Update(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var idResult = await _roleManager.UpdateAsync(role);

            idResult.ThrowIfInvalid();

            return true;
        }
    }

    public class RoleService<TRole> : RoleService<Guid, TRole>, IRoleService<TRole>
        where TRole : Role
    {
        public RoleService(RoleManager<TRole> roleManager) : base(roleManager)
        {
        }
    }
}
