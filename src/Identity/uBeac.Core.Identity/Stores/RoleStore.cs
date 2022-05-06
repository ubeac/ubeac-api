using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.Security.Claims;

namespace uBeac.Identity
{
    public class RoleStore<TRole, TRoleKey> :
        IRoleClaimStore<TRole>,
        IQueryableRoleStore<TRole>
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : Role<TRoleKey>
    {

        private readonly IRoleRepository<TRoleKey, TRole> _repository;

        public IQueryable<TRole> Roles => _repository.AsQueryable();

        public RoleStore(IRoleRepository<TRoleKey, TRole> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.Create(role, cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.Update(role, cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.Delete(role.Id, cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedRoleName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedRoleName;
            return Task.CompletedTask;
        }

        public async Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var id = roleId.GetTypedKey<TRoleKey>();
            return await _repository.GetById(id, cancellationToken);
        }

        public async Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var results = await _repository.Find(x => x.NormalizedName == normalizedRoleName, cancellationToken);

            return results.FirstOrDefault();
        }

        public async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default)
        {
            if (role.Claims is null)
                return await Task.FromResult(new List<Claim>());

            return role.Claims.Select(e => new Claim(e.ClaimType, e.ClaimValue)).ToList();
        }

        public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            if (role.Claims is null)
                role.Claims = new List<IdentityRoleClaim<TRoleKey>>();

            var currentClaim = role.Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);

            if (currentClaim == null)
            {
                var identityRoleClaim = new IdentityRoleClaim<TRoleKey>()
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                    RoleId = role.Id,
                    Id = 0
                };

                role.Claims.Add(identityRoleClaim);
            }
            return Task.CompletedTask;
        }

        public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            role.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class RoleStore<TRole> : RoleStore<TRole, Guid>
        where TRole : Role
    {
        public RoleStore(IRoleRepository<TRole> repository) : base(repository)
        {
        }
    }
}
