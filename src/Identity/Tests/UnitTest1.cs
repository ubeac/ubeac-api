using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Controllers
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UnitTest1()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
               .UseStartup<Startup>());

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            var x = await _client.GetAsync("/api/Account/GetAll");
            var z = await x.Content.ReadAsStringAsync();
            x.EnsureSuccessStatusCode();

        }
    }
}