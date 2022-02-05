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

public class RolesTests : BaseTestClass
{
    protected const string InsertUri = "/API/Roles/Insert";
    protected const string ReplaceUri = "/API/Roles/Replace";
    protected const string DeleteUri = "/API/Roles/Delete";
    protected const string AllUri = "/API/Roles/All";

    [Fact, TestPriority(1)]
    public async Task Insert_ReturnsSuccessApiResult()
    {
        var content = new StringContent(JsonConvert.SerializeObject(new Role
        {
            Name = "default"
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

        var result = await response.GetApiResult<IEnumerable<Role>>();
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Any());
    }

    [Fact, TestPriority(3)]
    public async Task Replace_ReturnsSuccessApiResult()
    {
        var roleId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IRoleRepository<Role>>().GetAll()).First().Id;
        var content = new StringContent(JsonConvert.SerializeObject(new Role
        {
            Id = roleId,
            Name = "default"
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(ReplaceUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }

    [Fact, TestPriority(4)]
    public async Task Delete_ReturnsSuccessApiResult()
    {
        var roleId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IRoleRepository<Role>>().GetAll()).First().Id;
        var content = new StringContent(JsonConvert.SerializeObject(new IdRequest
        {
            Id = roleId,
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(DeleteUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }
}