using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task ResetPassword_ShouldCallsResetPasswordMethodOfUserManager()
    {
        await _userService.ResetPassword(_testUserName, _testResetPasswordToken, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.ResetPasswordAsync(_testUser, _testResetPasswordToken, _testPassword), Times.Once);
    }

    [Fact]
    public async Task ResetPassword_ShouldCallsUpdateMethodOfUserManager()
    {
        await _userService.ResetPassword(_testUserName, _testResetPasswordToken, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.ResetPasswordAsync(_testUser, _testResetPasswordToken, _testPassword), Times.Once);
    }

    [Fact]
    public async Task ResetPassword_LastPasswordChangedAtShouldBeUpdated()
    {
        var before = _testUser.LastPasswordChangedAt;
        await _userService.ResetPassword(_testUserName, _testResetPasswordToken, _testPassword, _validToken);
        var after = _testUser.LastPasswordChangedAt;

        Assert.NotEqual(before, after);
    }

    [Fact]
    public async Task ResetPassword_LastPasswordChangedByShouldBeUpdated()
    {
        _applicationContextMock.Setup(applicationContext => applicationContext.UserName).Returns(_testUserName);

        var before = _testUser.LastPasswordChangedBy;
        await _userService.ResetPassword(_testUserName, _testResetPasswordToken, _testPassword, _validToken);
        var after = _testUser.LastPasswordChangedBy;

        Assert.NotEqual(before, after);
    }

    [Fact]
    public async Task ResetPassword_IfUserIsNotExist_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.ResetPassword(_incorrectTestUserName, _testResetPasswordToken, _testPassword, _validToken));
    }

    [Fact]
    public async Task ResetPassword_IfCancellationTokenIsCanceled_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.ResetPassword(_testUserName, _testResetPasswordToken, _testPassword, _canceledToken));
    }

    [Fact]
    public async Task ResetPassword_IfCancellationTokenIsCanceled_ShouldCancelsCallingResetPasswordMethodOfUserManager()
    {
        try { await _userService.ResetPassword(_testUserName, _testResetPasswordToken, _testPassword, _canceledToken); } catch { /* ignored */ }

        _userManagerMock.Verify(userManager => userManager.ResetPasswordAsync(_testUser, It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ResetPassword_IfCancellationTokenIsCanceled_ShouldCancelsCallingUpdateMethodOfUserManager()
    {
        try { await _userService.ResetPassword(_testUserName, _testResetPasswordToken, _testPassword, _canceledToken); } catch { /* ignored */ }

        _userManagerMock.Verify(userManager => userManager.UpdateAsync(_testUser), Times.Never);
    }
}