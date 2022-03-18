using System.ComponentModel;
using System.Data.Entity;
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
    protected readonly IEmailProvider EmailProvider;
    protected readonly IApplicationContext AppContext;

    public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider, IHttpContextAccessor httpContextAccessor, JwtOptions jwtOptions, IUserTokenRepository<TUserKey> userTokenRepository, IEmailProvider emailProvider, IApplicationContext appContext)
    {
        UserManager = userManager;
        JwtTokenProvider = jwtTokenProvider;
        HttpContextAccessor = httpContextAccessor;
        JwtOptions = jwtOptions;
        UserTokenRepository = userTokenRepository;
        EmailProvider = emailProvider;
        AppContext = appContext;
    }

    public virtual async Task Create(TUser user, string password, CancellationToken cancellationToken = default)
    {
        var identityResult = await UserManager.CreateAsync(user, password);
        identityResult.ThrowIfInvalid();
    }

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
        var user = await UserManager.FindByNameAsync(username);

        // Validate user password
        if (user is null || !await UserManager.CheckPasswordAsync(user, password)) throw new Exception("User doesn't exist or username/password is not valid!");

        // Generate tokens
        var token = JwtTokenProvider.GenerateToken<TUserKey, TUser>(user);
        var refreshToken = JwtTokenProvider.GenerateRefreshToken<TUserKey, TUser>(user);

        // Store refresh token
        var identityResult = await UserManager.SetAuthenticationTokenAsync(user, LOCAL_LOGIN_PROVIDER, REFRESH_TOKEN_NAME, refreshToken);
        identityResult.ThrowIfInvalid();

        // Update user properties related to login
        user.LastLoginAt = AppContext.Time;
        user.LoginsCount++;
        await UserManager.UpdateAsync(user);

        return new TokenResult<TUserKey>
        {
            UserId = user.Id,
            Roles = user.Roles,
            Token = token,
            RefreshToken = refreshToken
        };
    }

    public virtual async Task Update(TUser entity, CancellationToken cancellationToken = default)
    {
        await UserManager.UpdateAsync(entity);
    }

    public async Task<bool> ExistsUserName(string userName, CancellationToken cancellationToken = default)
    {
        userName = userName.ToUpperInvariant();
        return await UserManager.FindByNameAsync(userName) != null;
    }

    public async Task EnableOrDisable(TUserKey id, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user is null) throw new Exception("User doesn't exist!");

        user.Enabled = !user.Enabled;
        user.LastEnabledOrDisabledAt = AppContext.Time;
        user.LastEnabledOrDisabledBy = AppContext.UserName;

        var result = await UserManager.UpdateAsync(user);
        result.ThrowIfInvalid();
    }

    public virtual async Task<bool> Delete(TUserKey id, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user is null) throw new Exception("User doesn't exist!");

        var userRemoveResult = await UserManager.DeleteAsync(user);
        userRemoveResult.ThrowIfInvalid();

        return true;
    }

    public async Task<IEnumerable<TUser>> GetAll(CancellationToken cancellationToken = default)
    {
        // TODO: Check this: ToListAsync() is not working - throws exception! For this reason, the ToList() method is used
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

        // Update user properties related to password changing
        user.LastPasswordChangedAt = AppContext.Time;
        user.LastPasswordChangedBy = AppContext.UserName;
        await UserManager.UpdateAsync(user);
    }

    public virtual async Task ForgotPassword(string username, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User does not exist!");

        var resetPasswordToken = await UserManager.GeneratePasswordResetTokenAsync(user);
        await SendResetPasswordToken(user, resetPasswordToken);
    }

    protected virtual async Task SendResetPasswordToken(TUser user, string token)
    {
        // Send email in background to prevent in interruptions
        var backgroundWorker = new BackgroundWorker();
        backgroundWorker.DoWork += async (_, _) => await EmailProvider.Send(user.Email, ForgotPasswordMessage.Subject, ForgotPasswordMessage.GetBodyWithReplaces(token));
        backgroundWorker.RunWorkerAsync();
        await Task.CompletedTask;
    }

    public virtual async Task ResetPassword(string username, string token, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User does not exist!");

        var result = await UserManager.ResetPasswordAsync(user, token, newPassword);
        result.ThrowIfInvalid();

        // Update user properties related to password changing
        user.LastPasswordChangedAt = AppContext.Time;
        user.LastPasswordChangedBy = AppContext.UserName;
        await UserManager.UpdateAsync(user);
    }

    public virtual Task<TUserKey> GetCurrentUserId(CancellationToken cancellationToken = default)
    {
        if (HttpContextAccessor.HttpContext == null) return default;

        var userId = UserManager.GetUserId(HttpContextAccessor.HttpContext.User);
        return string.IsNullOrEmpty(userId) ? default : Task.FromResult(userId.GetTypedKey<TUserKey>());
    }

    public virtual async Task RevokeTokens(TUserKey id, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user == null) return;

        var identityResult = await UserManager.ResetAuthenticatorKeyAsync(user);
        identityResult.ThrowIfInvalid();

    }

    public virtual async Task<TokenResult<TUserKey>> RefreshToken(string refreshToken, string expiredToken, CancellationToken cancellationToken = default)
    {
        var principal = GetPrincipalFromExpiredToken(expiredToken);
        var username = principal?.Identity?.Name;
        var user = await UserManager.FindByNameAsync(username);
        var storedRefreshToken = await UserManager.GetAuthenticationTokenAsync(user, LOCAL_LOGIN_PROVIDER, REFRESH_TOKEN_NAME);
        if (storedRefreshToken != refreshToken) throw new Exception("Token expired!");

        var newToken = JwtTokenProvider.GenerateToken<TUserKey, TUser>(user);

        // Update user properties related to login
        user.LastLoginAt = AppContext.Time;
        user.LoginsCount++;
        await UserManager.UpdateAsync(user);

        return new TokenResult<TUserKey>
        {
            UserId = user.Id,
            Roles = user.Roles,
            Token = newToken,
            RefreshToken = refreshToken
        };
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true, // You might want to validate the audience and issuer depending on your use case
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.Secret)),
            ValidateLifetime = false, // Here we are saying that we don't care about the token's expiration date
            ValidAudience = JwtOptions.Audience,
            ValidIssuer = JwtOptions.Issuer
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        return principal;
    }
}

public class UserService<TUser> : UserService<Guid, TUser>, IUserService<TUser>
    where TUser : User
{
    public UserService(UserManager<TUser> userManager, IJwtTokenProvider jwtTokenProvider, IHttpContextAccessor httpContextAccessor, JwtOptions jwtOptions, IUserTokenRepository userTokenRepository, IEmailProvider emailProvider, IApplicationContext appContext) : base(userManager, jwtTokenProvider, httpContextAccessor, jwtOptions, userTokenRepository, emailProvider, appContext)
    {
    }
}