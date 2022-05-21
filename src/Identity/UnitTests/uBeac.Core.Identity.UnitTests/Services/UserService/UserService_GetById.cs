using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task GetById_ShouldReturnsUser()
    {
        var result = await _userService.GetById(_testUserId, _validToken);

        Assert.Equal(_testUser.Id, result.Id);
        Assert.Equal(_testUser.UserName, result.UserName);
    }

    [Fact]
    public async Task GetById_IfUserIsNotExist_ShouldReturnsNull()
    {
        var result = await _userService.GetById(_incorrectTestUserId, _validToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetById_IfCancellationTokenIsCanceled_ThrowsExceptionAndCancelsCallingFindByIdMethodOfUserManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.GetById(_testUserId, _canceledToken));

        _userManagerMock.Verify(userManager => userManager.FindByIdAsync(_testUserId.ToString()), Times.Never);
    }
}