namespace uBeac.Web.Identity;

public class UnitTypeViewModel<TKey> where TKey : IEquatable<TKey>
{ 
    public virtual TKey Id { get; set; }
    public virtual string Code { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
}

public class UnitTypeViewModel : UnitTypeViewModel<Guid>
{
}