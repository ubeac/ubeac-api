using Microsoft.Extensions.DependencyInjection;
using uBeac.Identity;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationExtensions
{
    public static WebApplication InsertUnits<TUnitKey, TUnit>(this WebApplication app, IEnumerable<TUnit> units)
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        var service = app.Services.GetService<IUnitService<TUnitKey, TUnit>>();

        if (service == null)
            throw new NullReferenceException("Service is not registered for unit");

        service.InsertOrUpdateMany(units);
        return app;
    }

    public static WebApplication InsertUnits<TUnit>(this WebApplication app, IEnumerable<TUnit> units)
        where TUnit : Unit
    {
        var service = app.Services.GetService<IUnitService<TUnit>>();

        if (service == null)
            throw new NullReferenceException("Service is not registered for unit");

        service.InsertOrUpdateMany(units);
        return app;
    }
}