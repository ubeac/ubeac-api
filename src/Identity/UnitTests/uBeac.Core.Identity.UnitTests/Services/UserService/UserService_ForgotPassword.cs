using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task ForgotPassword_ShouldCallsGeneratePasswordResetTokenOfUserManager()
    {
        await _userService.ForgotPassword(_testUserName, _validToken);

        _userManagerMock.Verify(userManager => userManager.GeneratePasswordResetTokenAsync(_testUser));
    }

    [Fact]
    public async Task ForgotPassword_IfUserIsNotExist_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.ForgotPassword(_incorrectTestUserName, _validToken));
    }

    [Fact]
    public async Task ForgotPassword_IfCancellationTokenIsCanceled_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.ForgotPassword(_testUserName, _canceledToken));
    }

    [Fact]
    public async Task ForgotPassword_IfCancellationTokenIsCanceled_ShouldCancelsCallingGeneratePasswordResetTokenMethodOfUserManager()
    {
        try { await _userService.ForgotPassword(_testUserName, _canceledToken); } catch { /* ignored */ }

        _userManagerMock.Verify(userManager => userManager.GeneratePasswordResetTokenAsync(_testUser), Times.Never);
    }
}