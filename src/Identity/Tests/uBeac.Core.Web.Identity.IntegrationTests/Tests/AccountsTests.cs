using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using uBeac.Web.Identity;
using uBeac.Web.Identity.IntegrationTests;
using Xunit;

namespace uBeac.Core.Web.Identity.IntegrationTests;

public class AccountsTests : BaseTestClass
{
    protected const string RegisterUri = "/API/Accounts/Register";

    [Fact, TestPriority(1)]
    public async Task Register_ReturnsSuccessApiResult()
    {
        var content = new StringContent(JsonConvert.SerializeObject(new RegisterRequest
        {
            Username = "amir",
            Email = "ap@ubeac.com",
            Password = "1qaz!QAZ"
        }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(RegisterUri, content);
        response.EnsureSuccessStatusCode();

        var result = await response.GetApiResult<bool>();
        Assert.True(result.Data);
    }
}