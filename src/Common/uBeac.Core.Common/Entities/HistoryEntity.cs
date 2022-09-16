namespace uBeac;

public interface IHistoryEntity<TData> : IEntity
{
    TData Data { get; set; }
    string ActionName { get; set; }
    DateTime CreatedAt { get; set; }
    IApplicationContext Context { get; set; }
}

public sealed class HistoryEntity<TData> : Entity, IHistoryEntity<TData>
{
    public TData Data { get; set; }
    public string ActionName { get; set; }
    public DateTime CreatedAt { get; set; }
    public IApplicationContext Context { get; set; }
}