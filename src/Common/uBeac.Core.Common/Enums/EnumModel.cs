namespace uBeac;

public class EnumModel
{
    public virtual string DisplayName { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual List<EnumValueModel> Values { get; set; }
}