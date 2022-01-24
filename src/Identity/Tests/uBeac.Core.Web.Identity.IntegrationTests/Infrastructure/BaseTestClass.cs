using System.Net.Http;

namespace uBeac.Web.Identity.IntegrationTests;

public abstract class BaseTestClass : PriorityOrderedTests
{
    protected readonly HttpClient Client;

    protected BaseTestClass()
    {
        Client = ApiFactory.Instance.CreateClient();
    }

    ~BaseTestClass()
    {
        Client?.Dispose();
    }
}