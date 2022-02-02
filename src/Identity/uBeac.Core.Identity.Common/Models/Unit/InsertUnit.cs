namespace uBeac.Identity;

public class InsertUnit
{
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual string Type { get; set; }
    public virtual string Description { get; set; }
    public virtual string ParentUnitId { get; set; }
}