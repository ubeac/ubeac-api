using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public partial class UserStore<TUser, TUserKey> : IUserAuthenticationTokenStore<TUser>
    {
        public Task SetTokenAsync(TUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            var token = user.Tokens.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);

            if (token == null)
            {
                token = new IdentityUserToken<TUserKey>
                {
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value,
                    UserId = user.Id
                };
                user.Tokens.Add(token);
            }
            else
            {
                token.Value = value;
            }
            return Task.CompletedTask;
        }

        public Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            var userTokens = user.Tokens ?? new List<IdentityUserToken<TUserKey>>();
            userTokens.RemoveAll(x => x.LoginProvider == loginProvider && x.Name == name);

            return Task.CompletedTask;
        }

        public Task<string> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            var userTokens = user.Tokens ?? new List<IdentityUserToken<TUserKey>>();
            var token = userTokens.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);
            return Task.FromResult(token == null ? string.Empty : token.Value);
        }
    }
}
