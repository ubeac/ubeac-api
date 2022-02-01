using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public class RoleService<TKey, TRole> : IRoleService<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey>
    {
        protected readonly RoleManager<TRole> RoleManager;

        public RoleService(RoleManager<TRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await RoleManager.FindByIdAsync(id.ToString());

            if (role == null) return false;

            var idResult = await RoleManager.DeleteAsync(role);

            idResult.ThrowIfInvalid();

            return true;
        }

        public virtual Task<bool> Exists(string roleName, CancellationToken cancellationToken = default)
        {
            return RoleManager.RoleExistsAsync(roleName);
        }

        public virtual Task<IEnumerable<TRole>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(RoleManager.Roles.AsEnumerable());
        }

        public virtual async Task Insert(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var idResult = await RoleManager.CreateAsync(role);

            idResult.ThrowIfInvalid();

        }

        public virtual async Task<bool> Update(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var idResult = await RoleManager.UpdateAsync(role);

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
