using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class RoleServiceTests
{
    [Fact]
    public async Task Create_ShouldCallsCreateMethodOfRoleManager()
    {
        await _roleService.Create(_testRole, _validToken);

        _roleManagerMock.Verify(roleManager => roleManager.CreateAsync(_testRole), Times.Once);
    }

    [Fact]
    public async Task Create_IfIdentityResultIsFailed_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _roleService.Create(_negativeTestRole, _validToken));
    }

    [Fact]
    public async Task Create_CanceledToken_ShouldThrowsExceptionAndCancelsCallingCreateMethodOfRoleManager()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _roleService.Create(_testRole, _canceledToken));

        _roleManagerMock.Verify(roleManager => roleManager.CreateAsync(_testRole), Times.Never);
    }
}