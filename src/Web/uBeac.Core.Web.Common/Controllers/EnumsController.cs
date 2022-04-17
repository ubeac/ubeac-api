using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web;

public class EnumsController : BaseController
{
    [HttpGet]
    public IListResult<EnumModel> GetAll()
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        var referencedAssemblies = assembly.GetReferencedAssemblies();
        var enums = referencedAssemblies.GetEnums();
        return enums.ToListResult();
    }
}