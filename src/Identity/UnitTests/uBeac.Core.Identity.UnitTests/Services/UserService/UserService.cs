using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    private readonly Guid _testUserId;
    private readonly string _testUserName;
    private readonly string _testPassword;
    private readonly User _testUser;

    private readonly Guid _negativeTestUserId;
    private readonly string _negativeTestUserName;
    private readonly User _negativeTestUser;

    private readonly CancellationToken _validToken = CancellationToken.None;
    private readonly CancellationToken _canceledToken = new(true);

    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<ITokenService<User>> _tokenServiceMock;
    private readonly Mock<IUserTokenRepository> _userTokenRepositoryMock;

    private readonly IUserService<User> _userService;

    public UserServiceTests()
    {
        _testUserId = Guid.NewGuid();
        _testUserName = "TestUserName";
        _testUser = new User(_testUserName) { Id = _testUserId };
        _testPassword = "1qaz!QAZ";

        _negativeTestUserId = Guid.NewGuid();
        _negativeTestUserName = "NegativeTestUserName";
        _negativeTestUser = new User(_negativeTestUserName) { Id = _negativeTestUserId };

        var userStoreMock = new Mock<IUserStore<User>>();

        _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(It.Is<User>(user => user.UserName == _testUserName), _testPassword)).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(_testUser, _testPassword)).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(userManager => userManager.CreateAsync(_negativeTestUser, _testPassword)).ReturnsAsync(IdentityResult.Failed());
        _userManagerMock.Setup(userManager => userManager.CreateAsync(It.Is<User>(user => user.UserName == _negativeTestUserName), _testPassword)).ReturnsAsync(IdentityResult.Failed());

        _tokenServiceMock = new Mock<ITokenService<User>>();

        _userTokenRepositoryMock = new Mock<IUserTokenRepository>();

        var emailProviderMock = new Mock<IEmailProvider>();
        var appContextMock = new Mock<IApplicationContext>();
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        _userService = new UserService<User>(_userManagerMock.Object, _tokenServiceMock.Object, _userTokenRepositoryMock.Object, emailProviderMock.Object, appContextMock.Object, httpContextAccessorMock.Object);
    }
}