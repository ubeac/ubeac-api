namespace uBeac;

public interface IDebugger
{
    void Add(object value);
    List<object> GetValues();
}
