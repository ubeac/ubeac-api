using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace uBeac.Identity;

public partial class UserServiceTests
{
    [Fact]
    public async Task GetCurrentUserId_IfHttpContextOfAccessorIsNull_ShouldThrowsException()
    {
        _httpContextAccessorMock.Setup(httpContextAccessor => httpContextAccessor.HttpContext).Returns(() => null);

        await Assert.ThrowsAsync<NullReferenceException>(async () => await _userService.GetCurrentUserId(_validToken));
    }

    [Fact]
    public async Task GetCurrentUserId_ShouldFetchesUserIdFromHttpContextAndUserManager()
    {
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(httpContext => httpContext.User).Returns(_testClaimsPrincipal);
        _httpContextAccessorMock.Setup(httpContextAccessor => httpContextAccessor.HttpContext).Returns(httpContextMock.Object);

        var userId = await _userService.GetCurrentUserId(_validToken);

        Assert.Equal(_testUserId, userId);
    }

    [Fact]
    public async Task GetCurrentUserId_IfUserIsNotAuthenticated_ShouldThrowsException()
    {
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(httpContext => httpContext.User).Returns(() => null);
        _httpContextAccessorMock.Setup(httpContextAccessor => httpContextAccessor.HttpContext).Returns(httpContextMock.Object);

        await Assert.ThrowsAsync<NullReferenceException>(async () => await _userService.GetCurrentUserId(_validToken));
    }

    [Fact]
    public async Task GetCurrentUserId_IfCancellationTokenIsCanceled_ThrowsException()
    {
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await _userService.GetCurrentUserId(_canceledToken));
    }
}