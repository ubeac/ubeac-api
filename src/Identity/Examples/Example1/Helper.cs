using uBeac.Identity;

namespace Example1;

public static class Helper
{
    public static DefaultUnitOptions<Unit> DefaultUnitOptions()
    {
        var headquarter = new Unit { Name = "Management Office", Code = "1000", Type = "HQ" };
        var tehranBranch = new Unit { Name = "Tehran Central Branch", Code = "4000", Type = "BH", ParentUnitId = headquarter.Id.ToString() };
        var mirdamadBranch = new Unit { Name = "Mirdamad Branch", Code = "40001", Type = "BR", ParentUnitId = tehranBranch.Id.ToString() };

        return new DefaultUnitOptions<Unit>()
        {
            Values = new List<Unit>
            {
                headquarter,
                tehranBranch,
                mirdamadBranch
            }
        };
    }

    public static DefaultUnitTypeOptions<UnitType> DefaultUnitTypeOptions()
    {
        return new DefaultUnitTypeOptions<UnitType>()
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