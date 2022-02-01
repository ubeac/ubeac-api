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

public class UsersTests : BaseTestClass
{
    protected const string InsertUri = "/API/Users/Insert";
    protected const string ReplaceUri = "/API/Users/Replace";
    protected const string ChangePasswordUri = "/API/Users/ChangePassword";
    protected const string FindUri = "/API/Users/Find";
    protected const string AllUri = "/API/Users/All";

    [Fact, TestPriority(1)]
    public async Task Insert_ReturnsSuccessApiResult()
    {
        var content = new StringContent(JsonConvert.SerializeObject(new InsertUserRequest
        {
            UserName = "john",
            Password = "1qaz!QAZ",
            PhoneNumber = "+98",
            PhoneNumberConfirmed = false,
            Email = "john@doe.com",
            EmailConfirmed = false
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(InsertUri, content);

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }

    [Fact, TestPriority(2)]
    public async Task All_ReturnsSuccessApiResult()
    {
        var response = await Client.GetAsync(AllUri);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<IEnumerable<User>>();
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Any());
    }

    [Fact, TestPriority(3)]
    public async Task Find_ReturnsSuccessApiResult()
    {
        var userId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IUserRepository<User>>().GetAll()).First().Id;
        var response = await Client.GetAsync($"{FindUri}?id={userId}");
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<User>();
        Assert.NotNull(result.Data);
        Assert.Equal(result.Data.Id, userId);
    }

    [Fact, TestPriority(4)]
    public async Task Replace_ReturnsSuccessApiResult()
    {
        var userId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IUserRepository<User>>().GetAll()).First().Id;
        var content = new StringContent(JsonConvert.SerializeObject(new ReplaceUserRequest
        {
            Id = userId,
            PhoneNumber = "+98",
            PhoneNumberConfirmed = true,
            Email = "john@doe.com",
            EmailConfirmed = true,
            LockoutEnabled = true,
            LockoutEnd = null
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(ReplaceUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }

    [Fact, TestPriority(5)]
    public async Task ChangePassword_ReturnsSuccessApiResult()
    {
        var userId = (await ApiFactory.Instance.Services.CreateScope().ServiceProvider.GetRequiredService<IUserRepository<User>>().GetAll()).First().Id;
        var content = new StringContent(JsonConvert.SerializeObject(new ChangePasswordRequest
        {
            UserId = userId,
            CurrentPassword = "1qaz!QAZ",
            NewPassword = "1QAZ!qaz"
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(ChangePasswordUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }
}