using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace uBeac.Web.Logging;

public class LogIgnoreResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty result = base.CreateProperty(member, memberSerialization);
        PropertyInfo property = member as PropertyInfo;

        LogIgnoreAttribute attribute = (LogIgnoreAttribute)result.AttributeProvider.GetAttributes(typeof(LogIgnoreAttribute), true).FirstOrDefault();

        if (attribute != null)
        {
            var x = GetType(attribute.Value);
            if (x == typeof(LogIgnoreAttribute))
                result.Ignored = true;
            else
                result.ValueProvider = new LogIgnoreValueProvider(property, attribute.Value);
        }            

        return result;
    }

    private Type GetType<T>(T obj) => typeof(T);

}

public class LogIgnoreValueProvider : IValueProvider
{
    private PropertyInfo _targetProperty;
    private object _value;

    public LogIgnoreValueProvider(PropertyInfo targetProperty, object value)
    {
        _targetProperty = targetProperty;
        _value = value;
    }

    public void SetValue(object target, object value)
    {
        _targetProperty.SetValue(target, value);
    }

    public object GetValue(object target)
    {
        return _value;        
    }
}
