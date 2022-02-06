using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using uBeac.Web;

namespace GettingStarted.IntegrationTests;

public static class Extensions
{
    public static async Task<ApiResult<TResult>> GetApiResult<TResult>(this HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<ApiResult<TResult>>(await response.Content.ReadAsStringAsync());
    }
}