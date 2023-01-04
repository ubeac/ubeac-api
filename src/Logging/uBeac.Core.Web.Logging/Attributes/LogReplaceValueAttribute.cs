namespace uBeac;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class LogReplaceValueAttribute : Attribute
{
    public LogReplaceValueAttribute(object value)
    {
        Value = value;
    }

    public object Value { get; set; }
}