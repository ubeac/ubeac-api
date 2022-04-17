using System.Reflection;

namespace uBeac;

public static class EnumExtensions
{
    public static IEnumerable<EnumModel> GetEnums(this Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(type => type.IsEnum && type.CustomAttributes.Any(attr => attr.AttributeType == typeof(EnumAttribute))))
        {
            var enumAttribute = type.GetCustomAttributes<EnumAttribute>().LastOrDefault() ?? new EnumAttribute();
            var enumModel = new EnumModel
            {
                DisplayName = enumAttribute.DisplayName,
                Name = type.Name,
                Description = enumAttribute.Description,
                Values = new List<EnumValueModel>()
            };

            foreach (var enumValue in Enum.GetValues(type))
            {
                var value = enumValue.ToString();
                var fieldInfo = type.GetField(value);
                var enumValueAttribute = fieldInfo.GetCustomAttributes<EnumValueAttribute>().LastOrDefault() ?? new EnumValueAttribute();

                enumModel.Values.Add(new EnumValueModel
                {
                    Value = enumValue,
                    DisplayName = enumValueAttribute.DisplayName,
                    Name = Enum.GetName(type, enumValue),
                    Description = enumValueAttribute.Description
                });
            }

            yield return enumModel;
        }
    }

    public static IEnumerable<EnumModel> GetEnums(this IEnumerable<AssemblyName> assemblyNames)
        => assemblyNames.Select(Assembly.Load).SelectMany(assembly => assembly.GetEnums());
}