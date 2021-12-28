using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public partial class UserStore<TUser, TUserKey> : IQueryableUserStore<TUser>
    {
        public IQueryable<TUser> Users => _repository.AsQueryable();
    }
}
