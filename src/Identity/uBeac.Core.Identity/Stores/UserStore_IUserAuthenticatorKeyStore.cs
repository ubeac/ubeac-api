using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public partial class UserStore<TUser, TUserKey> : IUserAuthenticatorKeyStore<TUser>
    {
        public Task<string?> GetAuthenticatorKeyAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AuthenticatorKey);
        }

        public Task SetAuthenticatorKeyAsync(TUser user, string key, CancellationToken cancellationToken)
        {
            user.AuthenticatorKey = key;
            return Task.CompletedTask;
        }
    }
}
