using Microsoft.AspNetCore.Mvc;

namespace uBeac.Web.Identity;

public static class TypeExtensions
{
    public static string GetAreaName(this Type type)
    {
        var areaAttribute = type.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(AreaAttribute));
        var areaNameValue = areaAttribute?.ConstructorArguments.FirstOrDefault().Value;
        if (areaNameValue == null) return string.Empty;
        return areaNameValue.ToString() ?? string.Empty;
    }
}