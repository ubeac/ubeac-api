using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace uBeac.Web.Identity.IntegrationTests;

public class A_RegisterTests : PriorityOrderedTests
{
    protected const string RegisterUri = "/API/Accounts/Register";

    [Fact, TestPriority(1)]
    public async Task Register_ReturnsSuccessStatusCode()
    {
        using var client = GetClient();
        var content = CreateRegisterRequestContent();
        var response = await SendRegisterRequest(client, content);
        response.EnsureSuccessStatusCode();
    }

    [Fact, TestPriority(2)]
    public async Task Register_AlreadyRegistered_ReturnsInternalServerErrorStatusCode()
    {
        using var client = GetClient();
        var content = CreateRegisterRequestContent();
        var response = await client.PostAsync(RegisterUri, content);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact, TestPriority(3)]
    public async Task Register_UserNameIsNull_ReturnsBadRequestStatusCode()
    {
        using var client = GetClient();
        var content = CreateRegisterRequestContent(userName: null);
        var response = await SendRegisterRequest(client, content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact, TestPriority(4)]
    public async Task Register_EmailIsNull_ReturnsBadRequestStatusCode()
    {
        using var client = GetClient();
        var content = CreateRegisterRequestContent(email: null);
        var response = await SendRegisterRequest(client, content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task Register_PasswordIsNull_ReturnsBadRequestStatusCode()
    {
        using var client = GetClient();
        var content = CreateRegisterRequestContent(password: null);
        var response = await SendRegisterRequest(client, content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    protected async Task<HttpResponseMessage> SendRegisterRequest(HttpClient client, StringContent content)
        => await client.PostAsync(RegisterUri, content);

    protected StringContent CreateRegisterRequestContent(string email = "ap@ubeac.io", string userName = "amir", string password = "P@ssw0rd")
    {
        var request = new RegisterRequest { Email = email, Username = userName, Password = password };
        return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
    }

    protected HttpClient GetClient() => ApiFactory.Instance.CreateClient();
}