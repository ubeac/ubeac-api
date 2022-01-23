using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace uBeac.Web.Identity.IntegrationTests;

public class B_LoginTests : PriorityOrderedTests
{
    protected const string LoginUri = "/API/Accounts/Login";

    [Fact, TestPriority(1)]
    public async Task Login_ReturnsSuccessStatusCode()
    {
        using var client = GetClient();
        var content = CreateLoginRequestContent();
        var response = await client.PostAsync(LoginUri, content);
        response.EnsureSuccessStatusCode();
    }

    [Fact, TestPriority(2)]
    public async Task Login_InvalidUserName_ReturnsInternalServerErrorStatusCode()
    {
        using var client = GetClient();
        var content = CreateLoginRequestContent(userName: "invalid");
        var response = await client.PostAsync(LoginUri, content);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Login_InvalidPassword_ReturnsInternalServerErrorStatusCode()
    {
        using var client = GetClient();
        var content = CreateLoginRequestContent(password: "password");
        var response = await client.PostAsync(LoginUri, content);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    protected async Task<HttpResponseMessage> SendLoginRequest(HttpClient client, StringContent content)
        => await client.PostAsync(LoginUri, content);

    protected StringContent CreateLoginRequestContent(string userName = "amir", string password = "P@ssw0rd")
    {
        var request = new LoginRequest { Username = userName, Password = password };
        return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
    }

    protected HttpClient GetClient() => ApiFactory.Instance.CreateClient();
}