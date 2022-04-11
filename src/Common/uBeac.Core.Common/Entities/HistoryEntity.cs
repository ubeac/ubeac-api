namespace uBeac;

public interface IHistoryEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
    object Data { get; set; }
    string ActionName { get; set; }
    DateTime CreatedAt { get; set; }
    IApplicationContext Context { get; set; }
}