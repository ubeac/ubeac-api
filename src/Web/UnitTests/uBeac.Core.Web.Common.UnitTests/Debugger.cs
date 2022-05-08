using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace uBeac.Web;

public class DebuggerTests
{
    private const string ItemsKey = "internalDebug";

    [Theory]
    [ClassData(typeof(TestData))]
    public void Value_Should_Be_Added_To_HttpContext_Items(object value)
    {
        var httpContextItems = new Dictionary<object, object>();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(httpContext => httpContext.Items).Returns(httpContextItems);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(httpContextAccessor => httpContextAccessor.HttpContext).Returns(httpContextMock.Object);

        var debugger = new Debugger(httpContextAccessorMock.Object);
        debugger.Add(value);

        var values = httpContextItems.First().Value as List<object>;

        Assert.NotNull(httpContextItems);
        Assert.NotEmpty(httpContextItems);
        
        Assert.NotNull(values);
        Assert.NotEmpty(values);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Values_Should_Fetched_From_HttpContext_Items(object value)
    {
        var httpContextItems = new Dictionary<object, object> { { ItemsKey, new List<object> { value } } };

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(httpContext => httpContext.Items).Returns(httpContextItems);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(httpContextAccessor => httpContextAccessor.HttpContext).Returns(httpContextMock.Object);

        var debugger = new Debugger(httpContextAccessorMock.Object);
        var values = debugger.GetValues();

        Assert.NotNull(values);
        Assert.NotEmpty(values);
    }

    private class TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "ThisIsTestValue" };
            yield return new object[] { 666 };
            yield return new object[] { new List<string> { "ThisIsTestValue" } };
            yield return new object[] { new { Value = "Test" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}