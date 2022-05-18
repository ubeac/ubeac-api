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
    private readonly Guid _spuriousTestRoleId;
    private readonly string _testRoleName;
    private readonly string _spuriousTestRoleName;
    private readonly Role _testRole;
    private readonly Role _spuriosTestRole;

    private readonly CancellationToken _validToken = CancellationToken.None;
    private readonly CancellationToken _canceledToken = new(true);

    public RoleServiceTests()
    {
        _testRoleId = Guid.NewGuid();
        _spuriousTestRoleId = Guid.NewGuid();
        _testRoleName = "TestRoleName";
        _spuriousTestRoleName = "SpuriousTestRoleName";
        _testRole = new Role(_testRoleName) { Id = _testRoleId };
        _spuriosTestRole = new Role(_spuriousTestRoleName) { Id = _spuriousTestRoleId }; 

        var roleStoreMock = new Mock<IRoleStore<Role>>();

        _roleManagerMock = new Mock<RoleManager<Role>>(roleStoreMock.Object, null, null, null, null);
        _roleManagerMock.Setup(roleManager => roleManager.FindByIdAsync(_testRoleId.ToString())).ReturnsAsync(_testRole);
        _roleManagerMock.Setup(roleManager => roleManager.FindByIdAsync(_spuriousTestRoleId.ToString())).ReturnsAsync(_spuriosTestRole);
        _roleManagerMock.Setup(roleManager => roleManager.RoleExistsAsync(_testRoleName)).ReturnsAsync(true);
        _roleManagerMock.Setup(roleManager => roleManager.RoleExistsAsync(_spuriousTestRoleName)).ReturnsAsync(false);
        _roleManagerMock.Setup(roleManager => roleManager.CreateAsync(_testRole)).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(roleManager => roleManager.UpdateAsync(_testRole)).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(roleManager => roleManager.DeleteAsync(_testRole)).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(roleManager => roleManager.CreateAsync(_spuriosTestRole)).ReturnsAsync(IdentityResult.Failed());
        _roleManagerMock.Setup(roleManager => roleManager.UpdateAsync(_spuriosTestRole)).ReturnsAsync(IdentityResult.Failed());
        _roleManagerMock.Setup(roleManager => roleManager.DeleteAsync(_spuriosTestRole)).ReturnsAsync(IdentityResult.Failed());

        _roleService = new RoleService<Role>(_roleManagerMock.Object);
    }
}