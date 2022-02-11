using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace uBeac.Identity;

public class UserService<TUserKey, TUser> : IUserService<TUserKey, TUser>
    where TUserKey : IEquatable<TUserKey>
    where TUser : User<TUserKey>
{
    const string LOCAL_LOGIN_PROVIDER = "local";
    const string REFRESH_TOKEN_NAME = "refreshToken";

    protected readonly UserManager<TUser> UserManager;
    protected readonly IJwtTokenProvider JwtTokenProvider;
    protected readonly IHttpContextAccessor HttpContextAccessor;
    protected readonly JwtOptions JwtOptions;
    protected readonly IUserTokenRepository<TUserKey> UserTokenRepository;

    public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider, IHttpContextAccessor httpContextAccessor, JwtOptions jwtOptions, IUserTokenRepository<TUserKey> userTokenRepository)
    {
        UserManager = userManager;
        JwtTokenProvider = jwtTokenProvider;
        HttpContextAccessor = httpContextAccessor;
        JwtOptions = jwtOptions;
        UserTokenRepository = userTokenRepository;
    }

    /// <summary>
    /// This method creates user: this should be used by high-level/admin users to create users
    /// Some user properties may be set by admin manually
    /// </summary>
    /// <param name = "user" ></ param >
    /// < param name="password"></param>
    /// <param name = "cancellationToken" ></ param >
    /// < returns ></ returns >
    public virtual async Task Insert(TUser user, string password, CancellationToken cancellationToken = default)
    {
        var identityResult = await UserManager.CreateAsync(user, password);
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

        await Insert(user, password, cancellationToken);

        return user;
    }

    public virtual async Task<TokenResult<TUserKey>> Authenticate(string username, string password, CancellationToken cancellationToken = default)
    {
        //validating user credentials
        var user = await UserManager.FindByNameAsync(username);

        if (user is null || !await UserManager.CheckPasswordAsync(user, password))
            throw new Exception("User doesn't exist or username/password is not valid!");

        //generating tokens
        var token = JwtTokenProvider.GenerateToken<TUserKey, TUser>(user);
        var refreshToken = JwtTokenProvider.GenerateRefreshToken<TUserKey, TUser>(user);

        // storing refresh token
        var identityResult = await UserManager.SetAuthenticationTokenAsync(user, LOCAL_LOGIN_PROVIDER, REFRESH_TOKEN_NAME, refreshToken);
        identityResult.ThrowIfInvalid();

        return new TokenResult<TUserKey>
        {
            UserId = user.Id,
            Token = token,
            RefreshToken = refreshToken
        };
    }

    public virtual async Task Replace(TUser entity, CancellationToken cancellationToken = default)
    {
        await UserManager.UpdateAsync(entity);
    }

    public virtual async Task<bool> Delete(TUserKey id, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user is null)
            throw new Exception("User doesn't exist!");

        var userRemoveResult = await UserManager.DeleteAsync(user);
        userRemoveResult.ThrowIfInvalid();

        return true;
    }

    public async Task<IEnumerable<TUser>> GetAll(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => UserManager.Users.ToList(), cancellationToken);
    }

    public virtual Task<TUser> GetById(TUserKey id, CancellationToken cancellationToken = default)
    {
        return UserManager.FindByIdAsync(id.ToString());
    }

    public virtual async Task ChangePassword(ChangePassword<TUserKey> changePassword, CancellationToken cancellationToken = default)
    {
        var user = await GetById(changePassword.UserId, cancellationToken);

        if (changePassword.UserId == null || user == null || !await UserManager.CheckPasswordAsync(user, changePassword.CurrentPassword))
            throw new Exception("User doesn't exist or username/password is not valid!");

        var idResult = await UserManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);

        idResult.ThrowIfInvalid();
    }

    public virtual async Task ForgotPassword(string username, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByNameAsync(username);
        if (user == null)
            throw new Exception("User does not exist!");

        var resetPasswordToken = await UserManager.GeneratePasswordResetTokenAsync(user);
        // todo: send email here

    }

    public virtual Task<TUserKey> GetCurrentUserId(CancellationToken cancellationToken = default)
    {
        if (HttpContextAccessor.HttpContext == null)
            return default;

        var userId = UserManager.GetUserId(HttpContextAccessor.HttpContext.User);
        if (string.IsNullOrEmpty(userId))
            return default;

        return Task.FromResult(userId.GetTypedKey<TUserKey>());
    }

    public virtual async Task RevokeTokens(TUserKey id, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user == null)
            return;

        var identityResult = await UserManager.ResetAuthenticatorKeyAsync(user);
        identityResult.ThrowIfInvalid();

    }

    public virtual async Task<TokenResult<TUserKey>> RefreshToken(string refreshToken, string expiredToken, CancellationToken cancellationToken = default)
    {
        var principal = GetPrincipalFromExpiredToken(expiredToken);
        var username = principal?.Identity?.Name;
        var user = await UserManager.FindByNameAsync(username);
        var storedRefreshToken = await UserManager.GetAuthenticationTokenAsync(user, LOCAL_LOGIN_PROVIDER, REFRESH_TOKEN_NAME);
        if (storedRefreshToken != refreshToken)
            throw new Exception("Token expired!");

        var newToken = JwtTokenProvider.GenerateToken<TUserKey, TUser>(user);

        return new TokenResult<TUserKey>
        {
            UserId = user.Id,
            Token = newToken,
            RefreshToken = refreshToken
        };
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.Secret)),
            ValidateLifetime = false, //here we are saying that we don't care about the token's expiration date,
            ValidAudience = JwtOptions.Audience,
            ValidIssuer = JwtOptions.Issuer
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        //var jwtSecurityToken = securityToken as JwtSecurityToken;
        //if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature))
        //    throw new SecurityTokenException("Invalid token");
        return principal;
    }
}

public class UserService<TUser> : UserService<Guid, TUser>, IUserService<TUser>
    where TUser : User
{
    public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider, IHttpContextAccessor httpContextAccessor, JwtOptions jwtOptions, IUserTokenRepository userTokenRepository) : base(userManager, jwtTokenProvider, httpContextAccessor, jwtOptions, userTokenRepository)
    {
    }
}