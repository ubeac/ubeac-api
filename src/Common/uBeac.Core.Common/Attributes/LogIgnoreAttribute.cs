namespace uBeac;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class LogIgnoreAttribute : Attribute
{
    public object Value { get; set; } = null;
    
    public LogIgnoreAttribute() {}

    public LogIgnoreAttribute(object value)
    {
        Value = value;
    }    
}
