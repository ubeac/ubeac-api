using System.Threading.Tasks;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class RoleServiceTests
{
    [Fact]
    public async Task Exists_ShouldCallsRoleExistsMethodOfRoleManager()
    {
        await _roleService.Exists(_testRoleName, _validToken);

        _roleManagerMock.Verify(roleManager => roleManager.RoleExistsAsync(_testRoleName), Times.Once);
    }

    [Fact]
    public async Task Exists_ReturnsTrue()
    {
        var result = await _roleService.Exists(_testRoleName, _validToken);

        Assert.True(result);
    }

    [Fact]
    public async Task Exists_ReturnsFalse()
    {
        var result = await _roleService.Exists(_negativeTestRoleName, _validToken);

        Assert.False(result);
    }
}