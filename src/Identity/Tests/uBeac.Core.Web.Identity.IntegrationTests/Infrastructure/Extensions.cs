using System.Net.Http;
using Newtonsoft.Json;

namespace uBeac.Web.Identity.IntegrationTests;

public static class Extensions
{
    public static ApiResult<TResult> GetApiResult<TResult>(this HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<ApiResult<TResult>>(response.Content.ReadAsStringAsync().Result);
    }
}