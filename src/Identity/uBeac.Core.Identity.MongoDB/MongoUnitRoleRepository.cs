using System.Linq.Expressions;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace uBeac.Identity.MongoDB;

public class MongoUnitRoleRepository<TUnitRoleKey, TUnitRole> : MongoEntityRepository<TUnitRoleKey, TUnitRole>, IUnitRoleRepository<TUnitRoleKey, TUnitRole>
    where TUnitRoleKey : IEquatable<TUnitRoleKey>
    where TUnitRole : UnitRole<TUnitRoleKey>
{
    public MongoUnitRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }

    public virtual async Task<TUnitRole> CorrectId(TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        await BeforeCorrectId(unitRole, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnitRole>(MatchIdentifiersExpression(unitRole));
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        var single = findResult.ToEnumerable(cancellationToken).SingleOrDefault();
        unitRole.Id = single == null ? default : single.Id;
        return unitRole;
    }

    protected virtual async Task BeforeCorrectId(TUnitRole unitRole, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    public virtual async Task<bool> Any(TUnitRole unitRole, CancellationToken cancellationToken = default)
    {
        await BeforeAny(unitRole, cancellationToken);
        var expressionFilter = new ExpressionFilterDefinition<TUnitRole>(MatchIdentifiersExpression(unitRole));
        var findResult = await Collection.FindAsync(expressionFilter, cancellationToken: cancellationToken);
        return await findResult.AnyAsync(cancellationToken);
    }

    protected virtual async Task BeforeAny(TUnitRole unitRole, CancellationToken cancellationToken)
    {
        ThrowIfCancelled(cancellationToken);
        await Task.CompletedTask;
    }

    protected virtual Expression<Func<TUnitRole, bool>> MatchIdentifiersExpression(TUnitRole unitRole) => unitRole.MatchIdentifiersExpression();

}

public class MongoUnitRoleRepository<TUnitRole> : MongoUnitRoleRepository<Guid, TUnitRole>, IUnitRoleRepository<TUnitRole>
    where TUnitRole : UnitRole
{
    public MongoUnitRoleRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}