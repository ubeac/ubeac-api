using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uBeac.Web.Identity;
using Xunit;

namespace PhoneBook.Identity.IntegrationTests;

public class DiscoveryTests : BaseTestClass, IClassFixture<Factory>
{
    private readonly Factory _factory;

    private const string ActionsUri = "API/Discovery/Actions";

    public DiscoveryTests(Factory factory)
    {
        _factory = factory;
    }

    [Fact, TestPriority(1)]
    public async Task Actions_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(ActionsUri);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<IEnumerable<ActionInfo>>();

        // Assert
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Any());
    }
}