using System.Linq.Expressions;

namespace uBeac;

public static class Expressions
{
    public static Expression<Func<TElement, bool>> ContainsExpression<TElement, TValue>(Expression<Func<TElement, TValue>> valueSelector, List<TValue> values)
    {
        if (null == valueSelector) { throw new ArgumentNullException(nameof(valueSelector)); }
        if (null == values) { throw new ArgumentNullException(nameof(values)); }

        var p = valueSelector.Parameters.Single();

        if (!values.Any()) { return e => false; }

        var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));

        var body = equals.Aggregate<Expression>(Expression.Or);
        return Expression.Lambda<Func<TElement, bool>>(body, p);
    }
}