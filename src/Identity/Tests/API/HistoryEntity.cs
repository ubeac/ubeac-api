namespace API;

public class HistoryEntity : IHistoryEntity<Guid>
{
    public Guid Id { get; set; }
    public object Data { get; set; }
    public string ActionName { get; set; }
    public DateTime CreatedAt { get; set; }
    public IApplicationContext Context { get; set; }
}