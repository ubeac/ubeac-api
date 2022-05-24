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

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_IfUserIsNull_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.ChangePassword(null, _testPassword, _testPassword, _validToken));
    }

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_IfCancellationTokenIsCanceled_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.ChangePassword(_testUser, _testPassword, _testPassword, _canceledToken));
    }

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_IfCancellationTokenIsCanceled_ShouldCancelsCallingChangePasswordMethodOfUserManager()
    {
        try { await _userService.ChangePassword(_testUser, _testPassword, _testPassword, _canceledToken); } catch { /* ignored */ }

        _userManagerMock.Verify(userManager => userManager.ChangePasswordAsync(_testUser, It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ChangePassword_IfCurrentPasswordIsIncorrect_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.ChangePassword(_testUser, _incorrectTestPassword, _testPassword, _validToken));
    }

    [Fact]
    public async Task ChangePassword_IfCurrentPasswordIsIncorrect_ShouldCancelsCallingChangePasswordMethodOfUserManager()
    {
        try { await _userService.ChangePassword(_testUser, _incorrectTestPassword, _testPassword, _validToken); } catch { /* ignored */ }

        _userManagerMock.Verify(userManager => userManager.ChangePasswordAsync(_testUser, It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_ShouldCallsChangePasswordMethodOfUserManager()
    {
        await _userService.ChangePassword(_testUser, _testPassword, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.ChangePasswordAsync(_testUser, _testPassword, _testPassword), Times.Once);
    }

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_IfIdentityResultIsFailed_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.ChangePassword(_negativeTestUser, _testPassword, _testPassword, _validToken));
    }

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_ShouldUpdatesLastPasswordChangedAt()
    {
        var before = _testUser.LastPasswordChangedAt;
        await _userService.ChangePassword(_testUser, _testPassword, _testPassword, _validToken);
        var after = _testUser.LastPasswordChangedAt;

        Assert.NotEqual(before, after);
    }

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_ShouldUpdatesLastPasswordChangedBy()
    {
        _applicationContextMock.Setup(applicationContext => applicationContext.UserName).Returns(_testUserName);

        var before = _testUser.LastPasswordChangedBy;
        await _userService.ChangePassword(_testUser, _testPassword, _testPassword, _validToken);
        var after = _testUser.LastPasswordChangedBy;

        Assert.NotEqual(before, after);
    }

    [Fact]
    public async Task ChangePassword_WithCurrentPassword_ShouldCallsUpdateMethodOfUserManager()
    {
        await _userService.ChangePassword(_testUser, _testPassword, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.UpdateAsync(_testUser), Times.Once);
    }
}