using System;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace uBeac.Identity;

public partial class RoleServiceTests
{
    private readonly Mock<RoleManager<Role>> _roleManagerMock;
    private readonly RoleService<Role> _roleService;

    private readonly Guid _testRoleId;
    private readonly string _testRoleName;
    private readonly Role _testRole;

    private readonly Guid _negativeTestRoleId;
    private readonly string _negativeTestRoleName;
    private readonly Role _negativeTestRole;

    private readonly CancellationToken _validToken = CancellationToken.None;
    private readonly CancellationToken _canceledToken = new(true);

    public RoleServiceTests()
    {
        _testRoleId = Guid.NewGuid();
        _negativeTestRoleId = Guid.NewGuid();
        _testRoleName = "TestRoleName";
        _negativeTestRoleName = "SpuriousTestRoleName";
        _testRole = new Role(_testRoleName) { Id = _testRoleId };
        _negativeTestRole = new Role(_negativeTestRoleName) { Id = _negativeTestRoleId }; 

        var roleStoreMock = new Mock<IRoleStore<Role>>();

        _roleManagerMock = new Mock<RoleManager<Role>>(roleStoreMock.Object, null, null, null, null);
        _roleManagerMock.Setup(roleManager => roleManager.FindByIdAsync(_testRoleId.ToString())).ReturnsAsync(_testRole);
        _roleManagerMock.Setup(roleManager => roleManager.FindByIdAsync(_negativeTestRoleId.ToString())).ReturnsAsync(_negativeTestRole);
        _roleManagerMock.Setup(roleManager => roleManager.RoleExistsAsync(_testRoleName)).ReturnsAsync(true);
        _roleManagerMock.Setup(roleManager => roleManager.RoleExistsAsync(_negativeTestRoleName)).ReturnsAsync(false);
        _roleManagerMock.Setup(roleManager => roleManager.CreateAsync(_testRole)).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(roleManager => roleManager.UpdateAsync(_testRole)).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(roleManager => roleManager.DeleteAsync(_testRole)).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(roleManager => roleManager.CreateAsync(_negativeTestRole)).ReturnsAsync(IdentityResult.Failed());
        _roleManagerMock.Setup(roleManager => roleManager.UpdateAsync(_negativeTestRole)).ReturnsAsync(IdentityResult.Failed());
        _roleManagerMock.Setup(roleManager => roleManager.DeleteAsync(_negativeTestRole)).ReturnsAsync(IdentityResult.Failed());

        _roleService = new RoleService<Role>(_roleManagerMock.Object);
    }
}