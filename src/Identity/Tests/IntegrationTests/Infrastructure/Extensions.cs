using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using uBeac;
using uBeac.Web;

namespace IntegrationTests;

public static class Extensions
{
    public static async Task<IResult<TResult>> GetApiResult<TResult>(this HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<Result<TResult>>(await response.Content.ReadAsStringAsync());
    }
}