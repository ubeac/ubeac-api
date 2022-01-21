using Microsoft.Extensions.DependencyInjection;
using uBeac.Identity;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationExtensions
{
    public static WebApplication UseDefaultUnits<TUnitKey, TUnit>(this WebApplication app, IEnumerable<TUnit> units)
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        var service = app.Services.GetService<IUnitService<TUnitKey, TUnit>>();

        if (service == null)
            throw new NullReferenceException("Service is not registered for unit");

        service.InsertOrUpdateMany(units);
        return app;
    }

    public static WebApplication UseDefaultUnits<TUnit>(this WebApplication app, IEnumerable<TUnit> units)
        where TUnit : Unit
    {
        var service = app.Services.GetService<IUnitService<TUnit>>();

        if (service == null)
            throw new NullReferenceException("Service is not registered for unit");

        service.InsertOrUpdateMany(units);
        return app;
    }

    public static WebApplication UseDefaultUnitTypes<TUnitTypeKey, TUnitType>(this WebApplication app, IEnumerable<TUnitType> unitTypes)
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {
        var service = app.Services.GetService<IUnitTypeService<TUnitTypeKey, TUnitType>>();

        if (service == null)
            throw new NullReferenceException("Service is not registered for unit type");

        service.InsertOrUpdateMany(unitTypes);
        return app;
    }

    public static WebApplication UseDefaultUnitTypes<TUnitType>(this WebApplication app, IEnumerable<TUnitType> unitTypes)
        where TUnitType : UnitType
    {
        var service = app.Services.GetService<IUnitTypeService<TUnitType>>();

        if (service == null)
            throw new NullReferenceException("Service is not registered for unit type");

        service.InsertOrUpdateMany(unitTypes);
        return app;
    }
}