using System.Linq.Expressions;

namespace uBeac.Identity;

public static class UnitIdentifiersExtensions
{
    public static Expression<Func<TUnit, bool>> EqualsExpression<TKey, TUnit>(this UnitIdentifiers identifiers)
        where TKey : IEquatable<TKey>
        where TUnit : Unit<TKey>
    {
        return unit => unit.Type == identifiers.Type && unit.Code == identifiers.Code;
    }

    public static Expression<Func<TUnit, bool>> EqualsExpression<TKey, TUnit>(this IEnumerable<UnitIdentifiers> identifiers)
        where TKey : IEquatable<TKey>
        where TUnit : Unit<TKey>
    {
        var types = identifiers.Select(identifier => identifier.Type).ToList();
        var typesFilter = Expressions.ContainsExpression<TUnit, string>(unit => unit.Type, types);
        var codes = identifiers.Select(identifier => identifier.Code).ToList();
        var codesFilter = Expressions.ContainsExpression<TUnit, string>(unit => unit.Code, codes);
        return typesFilter.And(codesFilter);
    }
}