using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public partial class UserStore<TUser, TUserKey> : IUserTwoFactorRecoveryCodeStore<TUser>
    {
        public Task ReplaceCodesAsync(TUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            var rcs = recoveryCodes.Select(x => new TwoFactorRecoveryCode { Code = x, Redeemed = false }).ToList();
            user.RecoveryCodes.Clear();
            user.RecoveryCodes.AddRange(rcs);
            return Task.CompletedTask;
        }

        public Task<bool> RedeemCodeAsync(TUser user, string code, CancellationToken cancellationToken)
        {
            var rc = user.RecoveryCodes.FirstOrDefault(x => x.Code == code);
            if (rc != null) rc.Redeemed = true;
            return Task.FromResult(true);
        }

        public Task<int> CountCodesAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.RecoveryCodes.Count);
        }
    }
}
