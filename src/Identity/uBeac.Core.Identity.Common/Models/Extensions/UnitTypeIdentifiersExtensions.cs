using System.Linq.Expressions;

namespace uBeac.Identity;

public static class UnitTypeIdentifiersExtensions
{
    public static Expression<Func<TUnitType, bool>> EqualsExpression<TKey, TUnitType>(this UnitTypeIdentifiers identifiers)
        where TKey : IEquatable<TKey>
        where TUnitType : UnitType<TKey>
    {
        return unitType => unitType.Code == identifiers.Code;
    }

    public static Expression<Func<TUnitType, bool>> EqualsExpression<TKey, TUnitType>(this IEnumerable<UnitTypeIdentifiers> identifiers)
        where TKey : IEquatable<TKey>
        where TUnitType : UnitType<TKey>
    {
        var codes = identifiers.Select(identifier => identifier.Code).ToList();
        var codesFilter = Expressions.ContainsExpression<TUnitType, string>(unit => unit.Code, codes);
        return codesFilter;
    }
}