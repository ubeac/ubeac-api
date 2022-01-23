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

    public static IEnumerable<UnitType> DefaultUnitTypes()
    {
        yield return new UnitType { Code = "HQ", Name = "Headquarter" };
        yield return new UnitType { Code = "BH", Name = "Branch" };
        yield return new UnitType { Code = "BR", Name = "Branch 2" };
    }
}