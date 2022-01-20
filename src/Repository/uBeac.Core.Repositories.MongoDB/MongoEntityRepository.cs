using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace uBeac.Repositories.MongoDB
{
    public class MongoEntityRepository<TKey, TEntity> : IEntityRepository<TKey, TEntity>
         where TKey : IEquatable<TKey>
         where TEntity : IEntity<TKey>
    {
        protected readonly IMongoCollection<TEntity> Collection;
        protected readonly IMongoCollection<BsonDocument> BsonCollection;
        protected readonly IMongoDatabase MongoDatabase;
        protected readonly IMongoDBContext MongoDbContext;

        public MongoEntityRepository(IMongoDBContext mongoDbContext)
        {
            MongoDatabase = mongoDbContext.Database;
            Collection = mongoDbContext.Database.GetCollection<TEntity>(GetCollectionName());
            BsonCollection = mongoDbContext.Database.GetCollection<BsonDocument>(GetCollectionName());
            MongoDbContext = mongoDbContext;
        }

        protected virtual string GetCollectionName()
        {
            return typeof(TEntity).Name;
        }

        public async Task ReplaceMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await BeforeReplaceMany(entities, cancellationToken);
            await Task.WhenAll(entities.Select(e => Replace(e, cancellationToken)));
        }

        protected virtual async Task BeforeReplaceMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            await BeforeDelete(id, cancellationToken);
            var idFilter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
            var deleteResult = await Collection.DeleteOneAsync(idFilter, cancellationToken);
            return deleteResult.DeletedCount == 1;
        }

        protected virtual async Task BeforeDelete(TKey id, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<long> DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            await BeforeDeleteMany(ids, cancellationToken);
            var idsFilter = Builders<TEntity>.Filter.In(x => x.Id, ids);
            var deleteResult = await Collection.DeleteManyAsync(idsFilter, cancellationToken);
            return deleteResult.DeletedCount;
        }

        protected virtual async Task BeforeDeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            await BeforeGetAll(cancellationToken);
            var filter = Builders<TEntity>.Filter.Empty;
            return (await Collection.FindAsync(filter, null, cancellationToken)).ToEnumerable(cancellationToken);
        }

        protected virtual async Task BeforeGetAll(CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            await BeforeGetById(id, cancellationToken);
            var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            return (await Collection.FindAsync(idFilter, null, cancellationToken)).SingleOrDefault(cancellationToken);
        }

        protected virtual async Task BeforeGetById(TKey id, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            await BeforeGetByIds(ids, cancellationToken);
            var idsFilter = Builders<TEntity>.Filter.In(x => x.Id, ids);
            return (await Collection.FindAsync(idsFilter, null, cancellationToken)).ToEnumerable(cancellationToken);
        }

        protected virtual async Task BeforeGetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            await BeforeInsert(entity, cancellationToken);
            await Collection.InsertOneAsync(entity, null, cancellationToken);
        }

        protected virtual async Task BeforeInsert(TEntity entity, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await BeforeInsertMany(entities, cancellationToken);
            await Collection.InsertManyAsync(entities, null, cancellationToken);
        }

        protected virtual async Task BeforeInsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<TEntity> Replace(TEntity entity, CancellationToken cancellationToken = default)
        {
            await BeforeReplace(entity, cancellationToken);
            var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            return await Collection.FindOneAndReplaceAsync(idFilter, entity, null, cancellationToken);
        }

        protected virtual async Task BeforeReplace(TEntity entity, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            await BeforeFind(filter, cancellationToken);
            var findResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
            return findResult.ToEnumerable(cancellationToken);
        }

        protected virtual async Task BeforeFind(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            await BeforeAny(filter, cancellationToken);
            var findResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
            return await findResult.AnyAsync(cancellationToken);
        }

        protected virtual async Task BeforeAny(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            ThrowIfCancelled(cancellationToken);
            await Task.CompletedTask;
        }

        public IQueryable<TEntity> AsQueryable() => Collection.AsQueryable();

        protected virtual void ThrowIfCancelled(CancellationToken cancellationToken)
            => cancellationToken.ThrowIfCancellationRequested();
    }

    public class MongoEntityRepository<TEntity> : MongoEntityRepository<Guid, TEntity>, IEntityRepository<TEntity>
        where TEntity : IEntity
    {
        public MongoEntityRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
