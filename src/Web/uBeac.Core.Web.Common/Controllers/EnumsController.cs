using Microsoft.AspNetCore.Mvc;
using uBeac.Enums;

namespace uBeac.Web;

public class EnumsController : BaseController
{
    private readonly IAssemblyEnumsProvider _assemblyEnums;

    public EnumsController(IAssemblyEnumsProvider assemblyEnums)
    {
        _assemblyEnums = assemblyEnums;
    }

    [HttpGet]
    public IListResult<EnumModel> GetAllByAssembly() => _assemblyEnums.GetAll().ToListResult();
}