using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task Register_ShouldCallsCreateMethodOfUserManager()
    {
        _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        await _userService.Register(_testUserName, null, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Register_IfIdentityResultIsFailed_ThrowsException()
    {
        _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

        await Assert.ThrowsAsync<Exception>(async () => await _userService.Register(_testUserName, null, _testPassword, _validToken));
    }

    [Fact]
    public async Task Register_CanceledToken_ShouldThrowsExceptionAndCancelsCallingCreateMethodOfUserManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.Register(_testUserName, null, _testPassword, _canceledToken));

        _userManagerMock.Verify(userManager => userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
    }
}