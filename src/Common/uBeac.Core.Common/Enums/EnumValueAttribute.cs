namespace uBeac;

[AttributeUsage(AttributeTargets.Field)]
public class EnumValueAttribute : Attribute
{
    public string DisplayName { get; set; }
    public string Description { get; set; }
}