using uBeac.Repositories;

namespace uBeac.Identity;
public interface IUserTokenRepository<TUserKey> : IEntityRepository<TUserKey, UserToken<TUserKey>>
   where TUserKey : IEquatable<TUserKey>
{

}
public interface IUserTokenRepository : IUserTokenRepository<Guid>
{
}
