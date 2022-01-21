using System.Linq.Expressions;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRepository<TUnitKey, TUnit> : MongoEntityRepository<TUnitKey, TUnit>, IUnitRepository<TUnitKey, TUnit>
    where TUnitKey : IEquatable<TUnitKey>
    where TUnit : Unit<TUnitKey>
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }

    public virtual async Task<TUnit> CorrectId(TUnit unit, CancellationToken cancellationToken = default)
    {
        await BeforeCorrectId(unit, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnit>(MatchIdentifiersExpression(unit));
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        var single = findResult.ToEnumerable(cancellationToken).SingleOrDefault();
        unit.Id = single == null ? default : single.Id;
        return unit;
    }

    protected virtual async Task BeforeCorrectId(TUnit unit, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<TUnit>> CorrectId(IEnumerable<TUnit> units, CancellationToken cancellationToken = default)
    {
        await BeforeCorrectId(units, cancellationToken);
        return await Task.WhenAll(units.Select(unit => CorrectId(unit, cancellationToken)));
    }

    protected virtual async Task BeforeCorrectId(IEnumerable<TUnit> units, CancellationToken cancellationToken = default)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    public virtual async Task<bool> Any(TUnit unit, CancellationToken cancellationToken = default)
    {
        await BeforeAny(unit, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnit>(MatchIdentifiersExpression(unit));
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        return await findResult.AnyAsync(cancellationToken);
    }

    protected virtual async Task BeforeAny(TUnit unit, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    protected virtual Expression<Func<TUnit, bool>> MatchIdentifiersExpression(TUnit unit) => unit.MatchIdentifiersExpression();
}

public class MongoUnitRepository<TUnit> : MongoUnitRepository<Guid, TUnit>, IUnitRepository<TUnit>
    where TUnit : Unit
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}
