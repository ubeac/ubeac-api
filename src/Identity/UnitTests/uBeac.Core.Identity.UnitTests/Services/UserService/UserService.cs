using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    private readonly Guid _testUserId;
    private readonly string _testUserName;
    private readonly string _incorrectTestUserName;
    private readonly string _testPassword;
    private readonly string _incorrectTestPassword;
    private readonly User _testUser;

    private readonly Guid _negativeTestUserId;
    private readonly string _negativeTestUserName;
    private readonly User _negativeTestUser;

    private readonly string _testResetPasswordToken;

    private readonly ClaimsPrincipal _testClaimsPrincipal;

    private readonly TokenResult _testTokenResult;

    private readonly CancellationToken _validToken = CancellationToken.None;
    private readonly CancellationToken _canceledToken = new(true);

    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<ITokenService<User>> _tokenServiceMock;
    private readonly Mock<IUserTokenRepository> _userTokenRepositoryMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

    private readonly IUserService<User> _userService;

    public UserServiceTests()
    {
        _testUserId = Guid.NewGuid();
        _testUserName = "TestUserName";
        _incorrectTestUserName = "IncorrectTestUserName";
        _testUser = new User(_testUserName) { Id = _testUserId, Roles = new List<string> { "USER" } };
        _testPassword = "1qaz!QAZ";
        _incorrectTestPassword = "1QAZ!qaz";

        _negativeTestUserId = Guid.NewGuid();
        _negativeTestUserName = "NegativeTestUserName";
        _negativeTestUser = new User(_negativeTestUserName) { Id = _negativeTestUserId };

        _testResetPasswordToken = "TestResetPasswordToken";

        _testClaimsPrincipal = new ClaimsPrincipal();

        _testTokenResult = new TokenResult
        {
            AccessToken = "ThisIsAccessToken",
            RefreshToken = "ThisIsRefreshToken",
            Expiry = DateTime.Now.AddMinutes(30)
        };

        var userStoreMock = new Mock<IUserStore<User>>();

        _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        _userManagerMock.Setup(userManager => userManager.FindByNameAsync(_testUserName)).ReturnsAsync(_testUser);
        _userManagerMock.Setup(userManager => userManager.FindByNameAsync(_negativeTestUserName)).ReturnsAsync(_negativeTestUser);
        _userManagerMock.Setup(userManager => userManager.GetUserId(_testClaimsPrincipal)).Returns(_testUserId.ToString);
        _userManagerMock.Setup(userManager => userManager.GetUserId(null)).Returns(() => null);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(It.Is<User>(user => user.UserName == _testUserName), _testPassword)).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(_testUser, _testPassword)).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(_negativeTestUser, _testPassword)).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(It.Is<User>(user => user.UserName == _negativeTestUserName), _testPassword)).ReturnsAsync(IdentityResult.Failed());
        _userManagerMock.Setup(userManager => userManager.CheckPasswordAsync(_testUser, _testPassword)).ReturnsAsync(true);
        _userManagerMock.Setup(userManager => userManager.CheckPasswordAsync(_testUser, _incorrectTestPassword)).ReturnsAsync(false);
        _userManagerMock.Setup(userManager => userManager.CheckPasswordAsync(_negativeTestUser, _testPassword)).ReturnsAsync(true);
        _userManagerMock.Setup(userManager => userManager.SetAuthenticationTokenAsync(_testUser, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(userManager => userManager.SetAuthenticationTokenAsync(_negativeTestUser, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
        _userManagerMock.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(_testUser)).ReturnsAsync(_testResetPasswordToken);
        _userManagerMock.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(_negativeTestUser)).ReturnsAsync(_testResetPasswordToken);
        _userManagerMock.Setup(userManager => userManager.ResetPasswordAsync(_testUser, _testResetPasswordToken, _testPassword)).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(userManager => userManager.ResetPasswordAsync(_negativeTestUser, _testResetPasswordToken, _testPassword)).ReturnsAsync(IdentityResult.Failed());

        _tokenServiceMock = new Mock<ITokenService<User>>();
        _tokenServiceMock.Setup(tokenService => tokenService.Generate(_testUser)).ReturnsAsync(_testTokenResult);
        _tokenServiceMock.Setup(tokenService => tokenService.Generate(_negativeTestUser)).ReturnsAsync(_testTokenResult);

        _userTokenRepositoryMock = new Mock<IUserTokenRepository>();

        var emailProviderMock = new Mock<IEmailProvider>();
        var appContextMock = new Mock<IApplicationContext>();

        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        _userService = new UserService<User>(_userManagerMock.Object, _tokenServiceMock.Object, _userTokenRepositoryMock.Object, emailProviderMock.Object, appContextMock.Object, _httpContextAccessorMock.Object);
    }
}