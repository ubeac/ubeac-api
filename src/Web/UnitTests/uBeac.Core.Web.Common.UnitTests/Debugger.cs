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

    private readonly Dictionary<object, object> _httpContextItems;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

    public DebuggerTests()
    {
        _httpContextItems = new Dictionary<object, object>();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(httpContext => httpContext.Items).Returns(_httpContextItems);

        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _httpContextAccessorMock.Setup(httpContextAccessor => httpContextAccessor.HttpContext).Returns(httpContextMock.Object);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Add_ValueShouldBeAddedToHttpContextItems(object expectedValue)
    {
        var debugger = new Debugger(_httpContextAccessorMock.Object);

        debugger.Add(expectedValue);

        var values = _httpContextItems?.FirstOrDefault().Value as List<object>;
        var actualValue = values?.FirstOrDefault();

        Assert.NotNull(_httpContextItems);
        Assert.NotEmpty(_httpContextItems);

        Assert.NotNull(values);
        Assert.NotEmpty(values);

        Assert.NotNull(actualValue);
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void GetValues_ValuesShouldFetchedFromHttpContextItems(object expectedValue)
    {
        var debugger = new Debugger(_httpContextAccessorMock.Object);

        var items = (List<object>)_httpContextItems[ItemsKey];
        items.Clear();
        items.Add(expectedValue);

        var values = debugger.GetValues();
        var actualValue = values.FirstOrDefault();

        Assert.NotNull(values);
        Assert.NotEmpty(values);
        Assert.Equal(expectedValue, actualValue);
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