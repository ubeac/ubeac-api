using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace uBeac.Identity
{
    public partial class UserStore<TUser, TUserKey> : IUserClaimStore<TUser>
    {
        public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                var identityClaim = new IdentityUserClaim<TUserKey>()
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                    UserId = user.Id
                };

                user.Claims.Add(identityClaim);
            }
            return Task.CompletedTask;
        }

        public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            var identityClaim = new IdentityUserClaim<TUserKey>()
            {
                ClaimType = newClaim.Type,
                ClaimValue = newClaim.Value,
                UserId = user.Id
            };

            user.Claims.Add(identityClaim);

            return Task.CompletedTask;
        }

        public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
                user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            return Task.CompletedTask;
        }

        public async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (await _repository.Find(u => u.Claims.Any(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value), cancellationToken)).ToList();
        }
    }
}
