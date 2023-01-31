using MongoDB.Bson;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace MongoDB.Driver;

internal static class FilterBuilderExtensions
{
    public static FilterDefinition<TDocument> Contains<TDocument>(this FilterDefinitionBuilder<TDocument> builder, Expression<Func<TDocument, object>> expression, string value)
        => builder.Regex(expression, new BsonRegularExpression(new Regex($".*{value}.*", RegexOptions.IgnoreCase)));
}
