global using System;
global using System.Threading.Tasks;
global using Xunit;
global using uBeac.Web.Identity;
global using uBeac.Identity;
global using uBeac.Web;
global using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Xunit.Sdk;
using Xunit.Abstractions;
using System.Linq;

namespace Identity
{
    [TestCaseOrderer("Identity.PriorityOrderer", "Identity")]
    public abstract class WebApiTestFixture<TStartup>
    {
        public const string BASE_API_URL = "/api";
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly IServiceProvider Services;

        public WebApiTestFixture()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            Services = Server.Services;

            Client = Server.CreateClient();
        }

        public TService GetServiceInstance<TService>()
        {
            return ActivatorUtilities.CreateInstance<TService>(Services);
        }

        public async Task<HttpResponseMessage> Post(string controllerName, string methodName, object postData)
        {
            var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            return await Client.PostAsync($"{BASE_API_URL}/{controllerName}/{methodName}", content);
        }
    }

    public static class Extentions
    {
        public static async Task<ApiResult<T>> GetApiResult<T>(this HttpResponseMessage message)
        {
            var content = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResult<T>>(content);
        }
    }

    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (TTestCase testCase in testCases)
            {
                int order = 0;

                foreach (IAttributeInfo attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(TestOrderAttribute).AssemblyQualifiedName)))
                    order = attr.GetNamedArgument<int>("Order");

                GetOrCreate(sortedMethods, order).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.Select(order => sortedMethods[order]))
            {
                list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach (TTestCase testCase in list)
                    yield return testCase;
            }
        }

        static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue result;

            if (dictionary.TryGetValue(key, out result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestOrderAttribute : Attribute
    {
        public TestOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; private set; }
    }
}
