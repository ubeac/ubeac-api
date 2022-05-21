using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class RoleServiceTests
{
    [Fact]
    public async Task Delete_ShouldCallsFindByIdMethodOfRoleManager()
    {
        await _roleService.Delete(_testRoleId, _validToken);

        _roleManagerMock.Verify(roleManager => roleManager.FindByIdAsync(_testRoleId.ToString()), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldCallsDeleteMethodOfRoleManager()
    {
        await _roleService.Delete(_testRoleId, _validToken);

        _roleManagerMock.Verify(roleManager => roleManager.DeleteAsync(_testRole), Times.Once);
    }

    [Fact]
    public async Task Delete_IfIdentityResultIsFailed_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _roleService.Delete(_negativeTestRoleId, _validToken));
    }

    [Fact]
    public async Task Delete_CanceledToken_ShouldThrowsExceptionAndCancelsCallingDeleteMethodOfRoleManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _roleService.Delete(_testRoleId, _canceledToken));

        _roleManagerMock.Verify(roleManager => roleManager.DeleteAsync(_testRole), Times.Never);
    }
}