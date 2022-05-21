using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task Create_ShouldCallsCreateMethodOfUserManager()
    {
        await _userService.Create(_testUser, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.CreateAsync(_testUser, _testPassword), Times.Once);
    }

    [Fact]
    public async Task Create_IfIdentityResultIsFailed_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.Create(_spuriosTestUser, _testPassword, _validToken));
    }

    [Fact]
    public async Task Create_CanceledToken_ShouldThrowsExceptionAndCancelsCallingCreateMethodOfUserManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.Create(_testUser, _testPassword, _canceledToken));

        _userManagerMock.Verify(userManager => userManager.CreateAsync(_testUser, _testPassword), Times.Never);
    }
}