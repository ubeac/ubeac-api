using uBeac.Identity;

namespace Example1;

public static class Helper
{
    public static IEnumerable<Unit> DefaultUnits()
    {
        var headquarter = new Unit(name: "Management Office", code: "1000", type: "HQ", parentUnitId: null);
        var tehranBranch = new Unit(name: "Tehran Central Branch", code: "4000", type: "BH", parentUnitId: headquarter.Id.ToString());
        var mirdamadBranch = new Unit(name: "Mirdamad Branch", code: "40001", type: "BR", parentUnitId: tehranBranch.Id.ToString());

        yield return headquarter;
        yield return tehranBranch;
        yield return mirdamadBranch;
    }
}