namespace uBeac.Identity.Seeders;

public class UnitTypeSeeder<TKey, TUnitType> : IDataSeeder
    where TKey : IEquatable<TKey>
    where TUnitType : UnitType<TKey>
{
    private readonly IUnitTypeService<TKey, TUnitType> _unitTypeService;
    private readonly UnitTypeOptions<TKey, TUnitType> _unitTypeOptions;

    public UnitTypeSeeder(IUnitTypeService<TKey, TUnitType> unitTypeService, UnitTypeOptions<TKey, TUnitType> unitTypeOptions)
    {
        _unitTypeService = unitTypeService;
        _unitTypeOptions = unitTypeOptions;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var data = _unitTypeOptions.DefaultValues?.ToList();

        if (data is null || data.Any() is false) return;

        foreach (var unitType in data)
        {
            if (await _unitTypeService.Exists(unitType.Code, cancellationToken) is false)
            {
                await _unitTypeService.Create(unitType, cancellationToken);
            }
        }
    }
}

public class UnitTypeSeeder<TUnitType> : UnitTypeSeeder<Guid, TUnitType>
    where TUnitType : UnitType
{
    public UnitTypeSeeder(IUnitTypeService<TUnitType> unitTypeService, UnitTypeOptions<TUnitType> unitTypeOptions) : base(unitTypeService, unitTypeOptions)
    {
    }
}
