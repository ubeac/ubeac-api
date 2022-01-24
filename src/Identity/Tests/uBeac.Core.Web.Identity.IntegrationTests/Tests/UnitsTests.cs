using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using uBeac.Identity;
using uBeac.Repositories.MongoDB;
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
    public async Task Insert_ReturnsSuccessStatusCode()
    {
        var content = new StringContent(JsonConvert.SerializeObject(new Unit
        {
            Id = Guid.NewGuid(),
            Name = "Headquarter",
            Code = "1",
            Type = "HQ"
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(InsertUri, content);
        response.EnsureSuccessStatusCode();
    }

    [Fact, TestPriority(2)]
    public async Task All_ReturnsSuccessStatusCode()
    {
        var response = await Client.GetAsync(AllUri);
        response.EnsureSuccessStatusCode();
    }

    [Fact, TestPriority(3)]
    public async Task Replace_ReturnsSuccessStatusCode()
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
    }

    [Fact, TestPriority(4)]
    public async Task Delete_ReturnsSuccessStatusCode()
    {
        var unitId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IUnitRepository<Unit>>().GetAll()).First().Id;
        var content = new StringContent(JsonConvert.SerializeObject(new Unit
        {
            Id = unitId
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(DeleteUri, content);
        response.EnsureSuccessStatusCode();
    }
}