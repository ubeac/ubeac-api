using MongoDB.Driver;
using uBeac.Identity;
using uBeac.Repositories.MongoDB;

namespace uBeac.Core.Identity.MongoDB;

public class MongoUnitTypeRepository<TUnitTypeKey, TUnitType> : MongoEntityRepository<TUnitTypeKey, TUnitType>, IUnitTypeRepository<TUnitTypeKey, TUnitType>
    where TUnitTypeKey : IEquatable<TUnitTypeKey>
    where TUnitType : UnitType<TUnitTypeKey>
{
    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }

    public virtual async Task<UnitTypeIdByIdentifiersResult<TUnitTypeKey>> GetId(UnitTypeIdentifiers identifiers, CancellationToken cancellationToken = default)
    {
        await BeforeGetId(identifiers, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnitType>(identifiers.EqualsExpression<TUnitTypeKey, TUnitType>());
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        var unitType = findResult.ToEnumerable(cancellationToken).Single();
        return new UnitTypeIdByIdentifiersResult<TUnitTypeKey>(unitType.Id, identifiers);
    }

    protected virtual async Task BeforeGetId(UnitTypeIdentifiers identifiers, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<UnitTypeIdByIdentifiersResult<TUnitTypeKey>>> GetIds(IEnumerable<UnitTypeIdentifiers> identifiers, CancellationToken cancellationToken = default)
    {
        await BeforeGetIds(identifiers, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnitType>(identifiers.EqualsExpression<TUnitTypeKey, TUnitType>());
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        var unitTypes = findResult.ToEnumerable(cancellationToken);
        return unitTypes.Select(GetIdResult);
    }

    protected virtual async Task BeforeGetIds(IEnumerable<UnitTypeIdentifiers> identifiers, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    public virtual async Task<bool> Any(UnitTypeIdentifiers identifiers, CancellationToken cancellationToken = default)
    {
        await BeforeAny(identifiers, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnitType>(identifiers.EqualsExpression<TUnitTypeKey, TUnitType>());
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        return await findResult.AnyAsync(cancellationToken);
    }

    protected virtual async Task BeforeAny(UnitTypeIdentifiers identifiers, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    protected virtual UnitTypeIdByIdentifiersResult<TUnitTypeKey> GetIdResult(TUnitType unitType) => unitType.GetIdResult<TUnitTypeKey, TUnitType>();
}

public class MongoUnitTypeRepository<TUnitType> : MongoUnitTypeRepository<Guid, TUnitType>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
{
    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}