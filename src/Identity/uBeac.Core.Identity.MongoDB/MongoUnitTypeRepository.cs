using System.Linq.Expressions;
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

    public virtual async Task<TUnitType> CorrectId(TUnitType unitType, CancellationToken cancellationToken = default)
    {
        await BeforeCorrectId(unitType, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnitType>(MatchIdentifiersExpression(unitType));
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        var single = findResult.ToEnumerable(cancellationToken).SingleOrDefault();
        unitType.Id = single == null ? default : single.Id;
        return unitType;
    }

    protected virtual async Task BeforeCorrectId(TUnitType unitType, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<TUnitType>> CorrectId(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default)
    {
        await BeforeCorrectId(unitTypes, cancellationToken);
        return await Task.WhenAll(unitTypes.Select(unitType => CorrectId(unitType, cancellationToken)));
    }

    public async Task BeforeCorrectId(IEnumerable<TUnitType> unitTypes, CancellationToken cancellationToken = default)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    public virtual async Task<bool> Any(TUnitType unitType, CancellationToken cancellationToken = default)
    {
        await BeforeAny(unitType, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnitType>(MatchIdentifiersExpression(unitType));
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        return await findResult.AnyAsync(cancellationToken);
    }

    protected virtual async Task BeforeAny(TUnitType unitType, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    protected virtual Expression<Func<TUnitType, bool>> MatchIdentifiersExpression(TUnitType unitType) => unitType.MatchIdentifiersExpression();
}

public class MongoUnitTypeRepository<TUnitType> : MongoUnitTypeRepository<Guid, TUnitType>, IUnitTypeRepository<TUnitType>
    where TUnitType : UnitType
{
    public MongoUnitTypeRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}