using Microsoft.AspNetCore.Cors.Infrastructure;

namespace uBeac.Web;

public class CorsPolicyOptions : CorsPolicy
{
    public string Name { get; set; }
}