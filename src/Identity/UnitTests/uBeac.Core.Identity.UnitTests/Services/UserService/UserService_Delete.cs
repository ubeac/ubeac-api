using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task Delete_ShouldCallsDeleteMethodOfUserManager()
    {
        await _userService.Delete(_testUserId, _validToken);

        _userManagerMock.Verify(userManager => userManager.DeleteAsync(_testUser), Times.Once);
    }

    [Fact]
    public async Task Delete_IfIdentityResultIsSucceeded_ShouldReturnsTrue()
    {
        var result = await _userService.Delete(_testUserId, _validToken);

        Assert.True(result);
    }

    [Fact]
    public async Task Delete_IfIdentityResultIsFailed_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.Delete(_negativeTestUserId, _validToken));
    }

    [Fact]
    public async Task Delete_IfUserIsNotExist_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.Delete(_incorrectTestUserId, _validToken));
    }
}