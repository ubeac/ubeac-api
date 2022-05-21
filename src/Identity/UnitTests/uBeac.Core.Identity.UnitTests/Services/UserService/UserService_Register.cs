using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task Register_ShouldCallsCreateMethodOfUserManager()
    {
        await _userService.Register(_testUserName, null, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.CreateAsync(It.Is<User>(user => user.UserName == _testUserName), _testPassword), Times.Once);
    }

    [Fact]
    public async Task Register_IfIdentityResultIsFailed_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.Register(_negativeTestUserName, null, _testPassword, _validToken));
    }

    [Fact]
    public async Task Register_CanceledToken_ShouldThrowsExceptionAndCancelsCallingCreateMethodOfUserManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.Register(_testUserName, null, _testPassword, _canceledToken));

        _userManagerMock.Verify(userManager => userManager.CreateAsync(It.Is<User>(user => user.UserName == _testUserName), It.IsAny<string>()), Times.Never);
    }
}