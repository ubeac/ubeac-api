using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task Authenticate_IfCancellationTokenIsCanceled_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.Authenticate(_testUserName, _testPassword, _canceledToken));
    }

    [Fact]
    public async Task Authenticate_ShouldCallsCheckPasswordMethodOfUserManager()
    {
        await _userService.Authenticate(_testUserName, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.CheckPasswordAsync(_testUser, _testPassword), Times.Once);
    }

    [Fact]
    public async Task Authenticate_IfUserNameIsIncorrect_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.Authenticate(_incorrectTestUserName, _testPassword, _validToken));
    }

    [Fact]
    public async Task Authenticate_IfPasswordIsIncorrect_ShouldThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.Authenticate(_testUserName, _incorrectTestPassword, _validToken));
    }

    [Fact]
    public async Task Authenticate_ShouldCallsGenerateMethodOfTokenService()
    {
        await _userService.Authenticate(_testUserName, _testPassword, _validToken);

        _tokenServiceMock.Verify(tokenService => tokenService.Generate(_testUser), Times.Once);
    }

    [Fact]
    public async Task Authenticate_SignInResultShouldMatchesWithTokenResult()
    {
        var signInResult = await _userService.Authenticate(_testUserName, _testPassword, _validToken);

        Assert.Equal(_testTokenResult.AccessToken, signInResult.Token);
        Assert.Equal(_testTokenResult.RefreshToken, signInResult.RefreshToken);
        Assert.Equal(_testTokenResult.Expiry, signInResult.Expiry);
    }

    [Fact]
    public async Task Authenticate_ShouldCallsSetAuthenticationTokenMethodOfUserManager()
    {
        await _userService.Authenticate(_testUserName, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.SetAuthenticationTokenAsync(_testUser, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Authenticate_IfResultOfSetAuthenticationTokenMethodOfUserManagerIsFailed_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _userService.Authenticate(_negativeTestUserName, _testPassword, _validToken));
    }

    [Fact]
    public async Task Authenticate_LastLoginAtShouldBeUpdated()
    {
        var before = _testUser.LastLoginAt;
        await _userService.Authenticate(_testUserName, _testPassword, _validToken);
        var after = _testUser.LastLoginAt;

        Assert.NotEqual(before, after);
    }

    [Fact]
    public async Task Authenticate_LoginsCountShouldBeUpdated()
    {
        var before = _testUser.LoginsCount;
        await _userService.Authenticate(_testUserName, _testPassword, _validToken);
        var after = _testUser.LoginsCount;

        Assert.NotEqual(before, after);
        Assert.Equal(before + 1, after);
    }

    [Fact]
    public async Task Authenticate_ShouldCallsUpdateMethodOfUserManager()
    {
        await _userService.Authenticate(_testUserName, _testPassword, _validToken);

        _userManagerMock.Verify(userManager => userManager.UpdateAsync(_testUser), Times.Once);
    }

    [Fact]
    public async Task Authenticate_SignInResultShouldMatchesWithUserInfo()
    {
        var signInResult = await _userService.Authenticate(_testUserName, _testPassword, _validToken);

        Assert.Equal(_testUser.Id, signInResult.UserId);
        Assert.Equal(_testUser.Roles, signInResult.Roles);
    }
}