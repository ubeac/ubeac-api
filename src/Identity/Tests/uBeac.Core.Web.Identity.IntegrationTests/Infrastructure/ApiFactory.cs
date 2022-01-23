using Microsoft.AspNetCore.Mvc.Testing;

namespace uBeac.Web.Identity.IntegrationTests;

internal sealed class ApiFactory : WebApplicationFactory<Program>
{
    private ApiFactory() { }

    private static ApiFactory _instance;
    public static ApiFactory Instance => _instance ??= new ApiFactory();
}