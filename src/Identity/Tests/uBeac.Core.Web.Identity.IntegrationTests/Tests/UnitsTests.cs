using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using uBeac.Identity;
using uBeac.Web;
using uBeac.Web.Identity;
using uBeac.Web.Identity.IntegrationTests;
using Xunit;

namespace uBeac.Core.Web.Identity.IntegrationTests;

public class UnitsTests : BaseTestClass
{
    protected const string InsertUri = "/API/Units/Insert";
    protected const string ReplaceUri = "/API/Units/Replace";
    protected const string DeleteUri = "/API/Units/Delete";
    protected const string AllUri = "/API/Units/All";

    [Fact, TestPriority(1)]
    public async Task Insert_ReturnsSuccessApiResult()
    {
        var content = new StringContent(JsonConvert.SerializeObject(new Unit
        {
            Name = "Headquarter",
            Code = "1",
            Type = "HQ"
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(InsertUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }

    [Fact, TestPriority(2)]
    public async Task All_ReturnsSuccessApiResult()
    {
        var response = await Client.GetAsync(AllUri);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<IEnumerable<Unit>>();
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Any());
    }

    [Fact, TestPriority(3)]
    public async Task Replace_ReturnsSuccessApiResult()
    {
        var unitId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IUnitRepository<Unit>>().GetAll()).First().Id;
        var content = new StringContent(JsonConvert.SerializeObject(new Unit
        {
            Id = unitId,
            Name = "Headquarter",
            Code = "1",
            Type = "HQ"
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(ReplaceUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }

    [Fact, TestPriority(4)]
    public async Task Delete_ReturnsSuccessApiResult()
    {
        var unitId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IUnitRepository<Unit>>().GetAll()).First().Id;
        var content = new StringContent(JsonConvert.SerializeObject(new IdRequest
        {
            Id = unitId
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(DeleteUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }
}