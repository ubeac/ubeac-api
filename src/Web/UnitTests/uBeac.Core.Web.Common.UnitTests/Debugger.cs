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
    private readonly Debugger _debugger;

    public DebuggerTests()
    {
        _httpContextItems = new Dictionary<object, object>();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(httpContext => httpContext.Items).Returns(_httpContextItems);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(httpContextAccessor => httpContextAccessor.HttpContext).Returns(httpContextMock.Object);

        _debugger = new Debugger(httpContextAccessorMock.Object);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Add_ValueShouldBeAddedToHttpContextItems(object expectedValue)
    {
        ClearValues();

        _debugger.Add(expectedValue);

        var values = GetValues();
        var actualValue = values.FirstOrDefault();

        Assert.NotNull(values);
        Assert.NotEmpty(values);

        Assert.NotNull(actualValue);
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void GetValues_ValuesShouldFetchedFromHttpContextItems(object expectedValue)
    {
        ClearValues();
        AddValue(expectedValue);

        var values = _debugger.GetValues();
        var actualValue = values.FirstOrDefault();

        Assert.NotNull(values);
        Assert.NotEmpty(values);

        Assert.NotNull(actualValue);
        Assert.Equal(expectedValue, actualValue);
    }

    private List<object> GetValues() => (List<object>)_httpContextItems[ItemsKey];
    private void AddValue(object value) => GetValues().Add(value);
    private void ClearValues() => GetValues().Clear();

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