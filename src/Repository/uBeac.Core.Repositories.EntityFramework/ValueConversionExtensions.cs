using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace uBeac.Repositories.EntityFramework;

public static class ValueConversionExtensions
{
    public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
    {
        var converter = new ValueConverter<T, string>
        (
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<T>(v) ?? new T()
        );

        var comparer = new ValueComparer<T>
        (
            (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
            v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
            v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v))
        );

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);

        return propertyBuilder;
    }

    public static PropertyBuilder<IApplicationContext> HasAppContextJsonConversion(this PropertyBuilder<IApplicationContext> propertyBuilder, Type appContextType)
    {
        if (!appContextType.IsAssignableTo(typeof(IApplicationContext))) throw new Exception("App context type is not valid.");

        var converter = new ValueConverter<IApplicationContext, string>
        (
            v => JsonConvert.SerializeObject(v),
            v => (IApplicationContext) JsonConvert.DeserializeObject(v, appContextType)
        );

        var comparer = new ValueComparer<IApplicationContext>
        (
            (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
            v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
            v => (IApplicationContext) JsonConvert.DeserializeObject(JsonConvert.SerializeObject(v), appContextType)
        );

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);

        return propertyBuilder;
    }
}