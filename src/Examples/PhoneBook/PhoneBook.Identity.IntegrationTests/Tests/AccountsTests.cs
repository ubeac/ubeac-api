using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using uBeac.Identity;
using uBeac.Web;
using uBeac.Web.Identity;
using Xunit;

namespace PhoneBook.Identity.IntegrationTests;

public class AccountsTests : BaseTestClass, IClassFixture<Factory>
{
    private readonly Factory _factory;

    public AccountsTests(Factory factory)
    {
        _factory = factory;
    }

    [Fact, TestPriority(1)]
    public async Task Register_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(new RegisterRequest
        {
            Username = "amir",
            Email = "ap@ubeac.com",
            Password = "1qaz!QAZ"
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(AccountStaticValues.RegisterUri, content);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<bool>();

        // Assert
        Assert.True(result.Data);
    }

    [Fact, TestPriority(2)]
    public async Task Login_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(new LoginRequest
        {
            Username = "amir",
            Password = "1qaz!QAZ"
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(AccountStaticValues.LoginUri, content);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<TokenResult>();

        // Assert
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.UserId);
        Assert.NotEmpty(result.Data.Token);
        Assert.NotEmpty(result.Data.RefreshToken);

        // Set Static Values
        AccountStaticValues.UserId = result.Data.UserId;
        AccountStaticValues.AccessToken = result.Data.Token;
        AccountStaticValues.RefreshToken = result.Data.RefreshToken;
    }

    [Fact, TestPriority(3)]
    public async Task RefreshToken_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(new RefreshTokenRequest
        {
            Token = AccountStaticValues.AccessToken,
            RefreshToken = AccountStaticValues.RefreshToken
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(AccountStaticValues.RefreshTokenUri, content);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<TokenResult>();

        // Set Static Values
        AccountStaticValues.UserId = result.Data.UserId;
        AccountStaticValues.AccessToken = result.Data.Token;
        AccountStaticValues.RefreshToken = result.Data.RefreshToken;
    }
}

public static class AccountStaticValues
{
    public const string RegisterUri = "/API/Accounts/Register";
    public const string LoginUri = "/API/Accounts/Login";
    public const string RefreshTokenUri = "/API/Accounts/RefreshToken";

    public static Guid UserId { get; set; }
    public static string AccessToken { get; set; }
    public static string RefreshToken { get; set; }
}