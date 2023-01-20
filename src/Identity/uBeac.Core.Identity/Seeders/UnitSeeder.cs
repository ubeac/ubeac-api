namespace uBeac.Identity.Seeders;

public class UnitSeeder<TKey, TUnit> : IDataSeeder
    where TKey : IEquatable<TKey>
    where TUnit : Unit<TKey>
{
    private readonly IUnitService<TKey, TUnit> _unitService;
    private readonly UnitOptions<TKey, TUnit> _unitOptions;

    public UnitSeeder(IUnitService<TKey, TUnit> unitService, UnitOptions<TKey, TUnit> unitOptions)
    {
        _unitService = unitService;
        _unitOptions = unitOptions;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var data = _unitOptions.DefaultValues?.ToList();

        if (data is null || data.Any() is false) return;

        foreach (var unit in data)
        {
            if (await _unitService.Exists(unit.Code, unit.Type, cancellationToken) is false)
            {
                var parent = unit.GetParentUnit();
                if (parent != null) unit.ParentUnitId = parent.Id;

                await _unitService.Create(unit, cancellationToken);
            }
        }
    }
}

public class UnitSeeder<TUnit> : UnitSeeder<Guid, TUnit>
    where TUnit : Unit
{
    public UnitSeeder(IUnitService<TUnit> unitService, UnitOptions<TUnit> unitOptions) : base(unitService, unitOptions)
    {
    }
}
