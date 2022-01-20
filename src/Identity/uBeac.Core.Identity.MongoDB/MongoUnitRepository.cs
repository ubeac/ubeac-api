﻿using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRepository<TUnitKey, TUnit> : MongoEntityRepository<TUnitKey, TUnit>, IUnitRepository<TUnitKey, TUnit>
    where TUnitKey : IEquatable<TUnitKey>
    where TUnit : Unit<TUnitKey>
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }

    public virtual async Task<UnitIdByIdentifiersResult<TUnitKey>> GetId(UnitIdentifiers identifiers, CancellationToken cancellationToken = default)
    {
        await BeforeGetId(identifiers, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnit>(identifiers.EqualsExpression<TUnitKey, TUnit>());
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        var unit = findResult.ToEnumerable(cancellationToken).Single();
        return new UnitIdByIdentifiersResult<TUnitKey>(unit.Id, identifiers);
    }

    protected virtual async Task BeforeGetId(UnitIdentifiers identifiers, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<UnitIdByIdentifiersResult<TUnitKey>>> GetIds(IEnumerable<UnitIdentifiers> identifiers, CancellationToken cancellationToken = default)
    {
        await BeforeGetIds(identifiers, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnit>(identifiers.EqualsExpression<TUnitKey, TUnit>());
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        var units = findResult.ToEnumerable(cancellationToken);
        return units.Select(unit => new UnitIdByIdentifiersResult<TUnitKey>(unit.Id, new UnitIdentifiers(unit.Code, unit.Type)));
    }

    protected virtual async Task BeforeGetIds(IEnumerable<UnitIdentifiers> identifiers, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.CompletedTask;
    }

    public virtual async Task<bool> Any(UnitIdentifiers identifiers, CancellationToken cancellationToken = default)
    {
        await BeforeAny(identifiers, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnit>(identifiers.EqualsExpression<TUnitKey, TUnit>());
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        return await findResult.AnyAsync(cancellationToken);
    }

    protected virtual async Task BeforeAny(UnitIdentifiers identifiers, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.CompletedTask;
    }
}

public class MongoUnitRepository<TUnit> : MongoUnitRepository<Guid, TUnit>, IUnitRepository<TUnit>
    where TUnit : Unit
{
    public MongoUnitRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}
