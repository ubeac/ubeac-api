namespace uBeac;

[AttributeUsage(AttributeTargets.Enum)]
public class EnumAttribute : Attribute
{
    public string DisplayName { get; set; }
    public string Description { get; set; }
}