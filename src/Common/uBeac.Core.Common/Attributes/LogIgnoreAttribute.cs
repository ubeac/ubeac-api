namespace uBeac;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class LogIgnoreAttribute : Attribute
{
    public object Value { get; set; } = (LogIgnoreAttribute)null;

    public LogIgnoreAttribute(object value)
    {
        Value = value;
    }
    public LogIgnoreAttribute()
    {
    }
}
