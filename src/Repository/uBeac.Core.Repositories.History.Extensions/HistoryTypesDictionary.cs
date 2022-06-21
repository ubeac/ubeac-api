namespace Microsoft.Extensions.DependencyInjection;

public interface IHistoryTypesDictionary : IDictionary<Type, List<Type>>
{
    void AddDataType(Type dataType);
    void AddRepositoryType(Type dataType, Type repositoryType);
}

public class HistoryTypesDictionary: Dictionary<Type, List<Type>>, IHistoryTypesDictionary
{
    public void AddDataType(Type dataType) => Add(dataType, new List<Type>());

    public void AddRepositoryType(Type dataType, Type repositoryType)
    {
        if (ContainsKey(dataType) is false) AddDataType(dataType);

        this[dataType].Add(repositoryType);
    }
}