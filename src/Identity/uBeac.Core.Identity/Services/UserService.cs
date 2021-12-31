using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace uBeac.Identity
{
    public class UserService<TUserKey, TUser> : IUserService<TUserKey, TUser>
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider)
        {
            _userManager = userManager;
            _jwtTokenProvider = jwtTokenProvider;
        }

        /// <summary>
        /// This method creates user: this should be used by high-level/admin users to create users
        /// Some user properties may be set by admin manualy
        /// </summary>
        /// <param name = "user" ></ param >
        /// < param name="password"></param>
        /// <param name = "cancellationToken" ></ param >
        /// < returns ></ returns >
        public virtual async Task Create(TUser user, string password, CancellationToken cancellationToken = default)
        {
            var identityResult = await _userManager.CreateAsync(user, password);
            identityResult.ThrowIfInvalid();
        }

        /// <summary>
        /// This method is for public users' registration, the email won't be confirmed
        /// A confirmation email will be sent to the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<TUser> Register(string username, string email, string password, CancellationToken cancellationToken = default)
        {
            var user = Activator.CreateInstance<TUser>();

            user.UserName = username;
            user.Email = email;
            user.EmailConfirmed = false;
            user.PhoneNumberConfirmed = false;

            await Create(user, password, cancellationToken);
            
            return user;
        }

        public virtual async Task<TokenResult<TUserKey>> Authenticate(string username, string password, CancellationToken cancellationToken = default)
        {
            //validating user credentials
            var user = await _userManager.FindByNameAsync(username);

            if (user is null || !await _userManager.CheckPasswordAsync(user, password))
                throw new Exception("User doesn't exist or username/password is not valid!");

            //generating token
            var token = _jwtTokenProvider.GenerateToken<TUserKey, TUser>(user);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // todo: refresh token
            return new TokenResult<TUserKey>(user.Id, tokenString, "", token.ValidTo);
        }

        public virtual async Task<bool> Delete(TUserKey id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
                throw new Exception("User doesn't exist!");

            var userRemoveResult = await _userManager.DeleteAsync(user);
            userRemoveResult.ThrowIfInvalid();

            return true;
        }

        public virtual Task<TUser> GetById(TUserKey id, CancellationToken cancellationToken = default)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }

        public virtual async Task ChangePassword(ChangePassword changePassword, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(changePassword.Username);
            if (user is null || !await _userManager.CheckPasswordAsync(user, changePassword.OldPassword))
                throw new Exception("User doesn't exist or username/password is not valid!");

            var idResult = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);

            idResult.ThrowIfInvalid();
        }

        public virtual async Task ForgotPassword(string username, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                throw new Exception("User doesn't exist!");

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            // todo: send email here

        }


    }

    public class UserService<TUser> : UserService<Guid, TUser>, IUserService<TUser>
        where TUser : User
    {
        public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider) : base(userManager, jwtTokenProvider)
        {
        }
    }
}
