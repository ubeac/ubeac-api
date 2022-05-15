using System;
using System.Collections.Generic;
using Xunit;

namespace uBeac.Common;

public class ResultExtensionsTests
{
    private readonly object _testObject = new { Test = "Value" };
    private readonly List<string> _testList = new()
    {
        "Amir", "Ali", "Mohammad", "Hesam"
    };
    private readonly Exception _testException = new();

    [Fact]
    public void ToListResult_Enumerable_ResultShouldNotBeNull()
    {
        var result = _testList.ToListResult();
        Assert.NotNull(result);
    }

    [Fact]
    public void ToListResult_Enumerable_ReturnsListResult_DataShouldNotBeNull()
    {
        var result = _testList.ToListResult();
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void ToListResult_Enumerable_ReturnsListResult_DataShouldEqualsWithInput()
    {
        var result = _testList.ToListResult();
        Assert.Equal(_testList, result.Data);
    }

    [Fact]
    public void ToListResult_Exception_ResultShouldNotBeNull()
    {
        var result = _testException.ToListResult<IEnumerable<string>>();
        Assert.NotNull(result);
    }

    [Fact]
    public void ToListResult_Exception_ReturnsListResult_DataShouldBeNull()
    {
        var result = _testException.ToListResult<IEnumerable<string>>();
        Assert.Null(result.Data);
    }

    [Fact]
    public void ToResult_Object_ResultShouldNotBeNull()
    {
        var result = _testObject.ToResult();
        Assert.NotNull(result);
    }

    [Fact]
    public void ToResult_Object_ReturnsResult_DataShouldNotBeNull()
    {
        var result = _testObject.ToResult();
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void ToResult_Object_ReturnsResult_DataShouldEqualsWithInput()
    {
        var result = _testObject.ToResult();
        Assert.Equal(_testObject, result.Data);
    }

    [Fact]
    public void ToResult_Exception_ResultShouldNotBeNull()
    {
        var result = _testException.ToResult<object>();
        Assert.NotNull(result);
    }

    [Fact]
    public void ToResult_Exception_ReturnsResult_DataShouldBeNull()
    {
        var result = _testException.ToResult<object>();
        Assert.Null(result.Data);
    }
}