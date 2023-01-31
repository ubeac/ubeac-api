using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using uBeac.Localization;
using uBeac.Web;

namespace Identity.MongoDB.API.Controllers;

public class LocalizationController : BaseController
{
    private readonly IStringLocalizer _localizer;
    private readonly ILocalizationService _localizationService;

    public LocalizationController(IStringLocalizer localizer, ILocalizationService localizationService)
    {
        _localizer = localizer;
        _localizationService = localizationService;
    }

    [HttpGet]
    public IResult<string> Test(string name = "Hesam")
    {
        return $"{_localizer["hi"]} {_localizer["welcome", name]}"
            .ToResult();
    }

    [HttpPost]
    public async Task Upsert(LocalizationValue value)
    {
        await _localizationService.Upsert(value);
    }

    [HttpPost]
    public async Task Delete(string key, string cultureName)
    {
        await _localizationService.Delete(key, cultureName);
    }
}
