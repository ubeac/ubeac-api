using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace uBeac.Web;

public class ApplicationContextTests
{
    private const string TraceId = "ThisIsTestTraceId";
    private const string SessionId = TraceId;
    private const string UserName = "ThisIsTestUserName";
    private const string UserIp = "127.0.0.1";
    private const string Language = "fa-IR";

    private readonly ApplicationContext _applicationContext;

    public ApplicationContextTests()
    {
        var requestHeaders = new HeaderDictionary
        {
            { "Accept-Language", Language }
        };

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(httpRequest => httpRequest.Headers).Returns(requestHeaders);

        var identityMock = new Mock<IIdentity>();
        identityMock.Setup(identity => identity.Name).Returns(UserName);

        var claimsPrincipalMock = new Mock<ClaimsPrincipal>();
        claimsPrincipalMock.Setup(claimsPrincipal => claimsPrincipal.Identity).Returns(identityMock.Object);

        var connectionInfoMock = new Mock<ConnectionInfo>();
        connectionInfoMock.Setup(connectionInfo => connectionInfo.RemoteIpAddress).Returns(IPAddress.Parse(UserIp));

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(context => context.Request).Returns(httpRequestMock.Object);
        httpContextMock.Setup(context => context.TraceIdentifier).Returns(TraceId);
        httpContextMock.Setup(context => context.User).Returns(claimsPrincipalMock.Object);
        httpContextMock.Setup(context => context.Connection).Returns(connectionInfoMock.Object);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(httpContextMock.Object);

        _applicationContext = new ApplicationContext(httpContextAccessorMock.Object);
    }

    [Fact]
    public void Constructor_PropertiesShouldNotBeNull()
    {
        Assert.NotNull( _applicationContext.TraceId);
        Assert.NotNull(_applicationContext.SessionId);
        Assert.NotNull(_applicationContext.UserName);
        Assert.NotNull(_applicationContext.UserIp);
        Assert.NotNull(_applicationContext.Language);
    }

    [Fact]
    public void Constructor_PropertiesShouldEqualsWithInputs()
    {
        Assert.Equal(TraceId, _applicationContext.TraceId);
        Assert.Equal(SessionId, _applicationContext.SessionId);
        Assert.Equal(UserName, _applicationContext.UserName);
        Assert.Equal(UserIp, _applicationContext.UserIp);
        Assert.Equal(Language, _applicationContext.Language);
    }
}