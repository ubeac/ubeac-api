using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace uBeac.Web;

public class ApplicationContextTests
{
    [Fact]
    public void ApplicationContext_Should_Be_Constructed_Using_HttpContextAccessor()
    {
        const string traceId = "ThisIsTestTraceId";
        const string sessionId = traceId;
        const string userName = "ThisIsTestUserName";
        const string userIp = "127.0.0.1";
        const string language = "fa-IR";

        var requestHeaders = new HeaderDictionary
        {
            { "Accept-Language", language }
        };

        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(httpRequest => httpRequest.Headers).Returns(requestHeaders);

        var identityMock = new Mock<IIdentity>();
        identityMock.Setup(identity => identity.Name).Returns(userName);

        var claimsPrincipalMock = new Mock<ClaimsPrincipal>();
        claimsPrincipalMock.Setup(claimsPrincipal => claimsPrincipal.Identity).Returns(identityMock.Object);
        
        var connectionInfoMock = new Mock<ConnectionInfo>();
        connectionInfoMock.Setup(connectionInfo => connectionInfo.RemoteIpAddress).Returns(IPAddress.Parse(userIp));

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(context => context.Request).Returns(httpRequestMock.Object);
        httpContextMock.Setup(context => context.TraceIdentifier).Returns(traceId);
        httpContextMock.Setup(context => context.User).Returns(claimsPrincipalMock.Object);
        httpContextMock.Setup(context => context.Connection).Returns(connectionInfoMock.Object);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(httpContextMock.Object);

        var appContext = new ApplicationContext(httpContextAccessorMock.Object);
        Assert.Equal(traceId, appContext.TraceId);
        Assert.Equal(sessionId, appContext.SessionId);
        Assert.Equal(userName, appContext.UserName);
        Assert.Equal(userIp, appContext.UserIp);
        Assert.Equal(language, appContext.Language);
    }
}