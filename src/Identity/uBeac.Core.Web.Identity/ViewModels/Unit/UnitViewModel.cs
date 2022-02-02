namespace uBeac.Web.Identity;

public class UnitViewModel<TKey> where TKey : IEquatable<TKey>
{ 
    public virtual TKey Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual string Type { get; set; }
    public virtual string Description { get; set; }
    public virtual string ParentUnitId { get; set; }
}

public class UnitViewModel : UnitTypeViewModel<Guid>
{
}