using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public partial class UserStore<TUser, TUserKey> : IProtectedUserStore<TUser>
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        private readonly IUserRepository<TUserKey, TUser> _repository;

        public UserStore(IUserRepository<TUserKey, TUser> repository)
        {
            _repository = repository;
        }

        static T ChangeType<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }

    public class UserStore<TUser>: UserStore<TUser, Guid>
        where TUser : User
    {
        public UserStore(IUserRepository<TUser> repository):base(repository)
        {
        }
    }
}
