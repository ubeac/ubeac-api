using System.Linq.Expressions;
using System.Reflection;

namespace uBeac.Identity;

public static class IdentifierExtensions
{
    private static List<PropertyInfo> GetPropertyInfos<TElement>(this TElement element) where TElement : class
        => element.GetType().GetProperties(BindingFlags.Public)
            .Where(property => property.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(IdentifierAttribute)))
            .ToList();

    public static Expression<Func<TElement, bool>> MatchIdentifiersExpression<TElement>(this TElement element) where TElement : class
    {
        var propertyInfos = GetPropertyInfos(element);
        if (!propertyInfos.Any()) return default;

        var expressions = new List<Expression>();
        var type = element.GetType();
        var parameter = Expression.Parameter(type);
        foreach (var propertyInfo in propertyInfos)
        {
            var property = Expression.Property(parameter, propertyInfo);
            var propertyValue = propertyInfo.GetValue(element);
            var target = Expression.Constant(propertyValue);
            var expression = Expression.Equal(property, target);
            expressions.Add(expression);
        }

        var body = expressions.Aggregate(Expression.And);
        return Expression.Lambda<Func<TElement, bool>>(body, parameter);
    }
}