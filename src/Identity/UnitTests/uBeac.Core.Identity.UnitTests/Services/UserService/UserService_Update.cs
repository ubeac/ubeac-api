using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task Update_ShouldCallsUpdateMethodOfUserManager()
    {
        await _userService.Update(_testUser, _validToken);

        _userManagerMock.Verify(userManager => userManager.UpdateAsync(_testUser), Times.Once);
    }

    [Fact]
    public async Task Update_IfCancellationTokenIsCanceled_ShouldThrowsExceptionAndCancelCallingUpdateMethodOfUserManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.Update(_testUser, _canceledToken));

        _userManagerMock.Verify(userManager => userManager.UpdateAsync(_testUser), Times.Never);
    }
}