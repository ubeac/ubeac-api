using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web;

public class EnumsController : BaseController
{
    private readonly IEnumProvider _enumProvider;

    public EnumsController(IEnumProvider enumProvider)
    {
        _enumProvider = enumProvider;
    }

    [HttpGet]
    public IListResult<EnumModel> GetAll() => _enumProvider.ExposeEnums().ToListResult();
}