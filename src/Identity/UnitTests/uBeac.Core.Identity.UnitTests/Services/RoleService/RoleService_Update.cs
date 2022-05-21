using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class RoleServiceTests
{
    [Fact]
    public async Task Update_ShouldCallsUpdateMethodOfRoleManager()
    {
        await _roleService.Update(_testRole, _validToken);

        _roleManagerMock.Verify(roleManager => roleManager.UpdateAsync(_testRole), Times.Once);
    }

    [Fact]
    public async Task Update_IfIdentityResultIsFailed_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _roleService.Update(_negativeTestRole, _validToken));
    }

    [Fact]
    public async Task Update_CanceledToken_ShouldThrowsExceptionAndCancelsCallingUpdateMethodOfRoleManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _roleService.Update(_testRole, _canceledToken));

        _roleManagerMock.Verify(roleManager => roleManager.UpdateAsync(_testRole), Times.Never);
    }
}