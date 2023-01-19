using Microsoft.Extensions.Localization;

namespace uBeac.Localization;

public class Localizer : IStringLocalizer
{
    private readonly ILocalizationService _service;
    private readonly IApplicationContext _context;

    public Localizer(ILocalizationService service, IApplicationContext context)
    {
        _service = service;
        _context = context;
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var values = _service.GetAllByCultureName(_context.Language).Result;

        return values.Select(x => new LocalizedString(x.Key, x.Value, false));
    }

    public LocalizedString this[string name]
    {
        get
        {
            var exists = _service.ExistsValue(name, _context.Language).Result;
            var value = exists ? _service.GetValue(name, _context.Language).Result.Value : name;

            return new LocalizedString(name, value, !exists);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = this[name];

            if (value.ResourceNotFound) return value;
            var formattedValue = string.Format(value.Value, arguments);

            return new LocalizedString(name, formattedValue, false);
        }
    }
}
