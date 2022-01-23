using uBeac.Identity;

namespace Example1;

public static class Helper
{
    public static IEnumerable<Unit> DefaultUnits()
    {
        var headquarter = new Unit { Name = "Management Office", Code = "1000", Type = "HQ" };
        var tehranBranch = new Unit { Name = "Tehran Central Branch", Code = "4000", Type = "BH", ParentUnitId = headquarter.Id.ToString() };
        var mirdamadBranch = new Unit { Name = "Mirdamad Branch", Code = "40001", Type = "BR", ParentUnitId = tehranBranch.Id.ToString() };
        yield return headquarter;
        yield return tehranBranch;
        yield return mirdamadBranch;
    }

    public static DefaultUnitTypes<UnitType> DefaultUnitTypes()
    {
        return new DefaultUnitTypes<UnitType>()
        {
            Values = new List<UnitType>
            {
                new UnitType { Code = "HQ", Name = "Headquarter", Description = "Desc 1" },
                new UnitType { Code = "BH", Name = "Branch", Description = "Desc 1" },
                new UnitType { Code = "BR", Name = "Branch 2", Description = "Desc 1" }
            }
        };
    }
}