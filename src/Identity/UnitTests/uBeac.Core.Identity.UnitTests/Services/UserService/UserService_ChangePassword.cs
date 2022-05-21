using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task ChangePassword_ShouldCallsGeneratePasswordResetTokenOfUserManager()
    {
        await _userService.ChangePassword(_testUser, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.GeneratePasswordResetTokenAsync(_testUser), Times.Once);
    }

    [Fact]
    public async Task ChangePassword_ShouldCallsResetPasswordMethodOfUserManager()
    {
        await _userService.ChangePassword(_testUser, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.ResetPasswordAsync(_testUser, _testResetPasswordToken, _testPassword), Times.Once);
    }

    [Fact]
    public async Task ChangePassword_IfResultOfResetPasswordMethodOfUserManagerIsFailed_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.ChangePassword(_negativeTestUser, _testPassword, _validToken));
    }

    [Fact]
    public async Task ChangePassword_IfCancellationTokenIsCanceled_ShouldThrowsExceptionAndCancelCallingResetPasswordMethodOfUserManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.ChangePassword(_testUser, _testPassword, _canceledToken));

        _userManagerMock.Verify(userManager => userManager.ResetPasswordAsync(_testUser, It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}