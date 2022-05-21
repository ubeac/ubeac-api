using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task RevokeTokens_ShouldCallsResetAuthenticatorKeyMethodOfUserManager()
    {
        await _userService.RevokeTokens(_testUserId, _validToken);

        _userManagerMock.Verify(userManager => userManager.ResetAuthenticatorKeyAsync(_testUser), Times.Once);
    }

    [Fact]
    public async Task RevokeTokens_IfIdentityResultIsFailed_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.RevokeTokens(_negativeTestUserId, _validToken));
    }

    [Fact]
    public async Task RevokeTokens_IfUserIsNotExist_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.RevokeTokens(_incorrectTestUserId, _validToken));
    }

    [Fact]
    public async Task RevokeTokens_IfCancellationTokenIsCanceled_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.RevokeTokens(_testUserId, _canceledToken));
    }

    [Fact]
    public async Task RevokeTokens_IfCancellationTokenIsCanceled_ShouldCancelsCallingResetAuthenticatorKeyMethodOfUserManager()
    {
        try { await _userService.RevokeTokens(_testUserId, _canceledToken); } catch { /* ignored */ }

        _userManagerMock.Verify(userManager => userManager.ResetAuthenticatorKeyAsync(_testUser), Times.Never);
    }
}