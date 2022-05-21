using System;
using System.Threading.Tasks;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task ExistsUserName_IfUserIsExist_ReturnsTrue()
    {
        var result = await _userService.ExistsUserName(_testUserName, _validToken);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsUserName_IfUserIsNotExist_ReturnsFalse()
    {
        var result = await _userService.ExistsUserName(_incorrectTestUserName, _validToken);
        
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsUserName_IfCancellationTokenIsCanceled_ThrowsException()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.ExistsUserName(_testUserName, _canceledToken));
    }
}