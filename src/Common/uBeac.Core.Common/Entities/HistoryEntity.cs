namespace uBeac;

public class HistoryEntity<TData> : Entity
{
    public virtual TData Data { get; set; }
    public virtual string DataId { get; set; }
    public virtual string ActionName { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual IApplicationContext Context { get; set; }
}

public class HistoryEntity : HistoryEntity<object>
{
}