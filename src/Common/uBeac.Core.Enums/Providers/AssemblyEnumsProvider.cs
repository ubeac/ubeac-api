using System.Reflection;

namespace uBeac.Enums;

public class AssemblyEnumsProvider : IAssemblyEnumsProvider
{
    private readonly IEnumerable<EnumModel> _enums;

    public AssemblyEnumsProvider(Assembly assembly)
    {
        _enums = ExposeAssemblyEnums(assembly);
    }

    public AssemblyEnumsProvider(IEnumerable<Assembly> assemblies)
    {
        _enums = ExposeAssemblyEnums(assemblies);
    }

    public IEnumerable<EnumModel> GetAll() => _enums;

    public static IEnumerable<EnumModel> ExposeAssemblyEnums(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(type =>
                     type.IsEnum && type.CustomAttributes.Any(attr => attr.AttributeType == typeof(EnumAttribute))))
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

    public static IEnumerable<EnumModel> ExposeAssemblyEnums(IEnumerable<Assembly> assemblies)
        => assemblies.SelectMany(ExposeAssemblyEnums);
}