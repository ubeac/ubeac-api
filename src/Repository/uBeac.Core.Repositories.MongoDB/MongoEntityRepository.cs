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

        public virtual async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idFilter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
            var deleteResult = await Collection.DeleteOneAsync(idFilter, cancellationToken);
            return deleteResult.DeletedCount == 1;
        }

        public virtual async Task<long> DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            var idsFilter = Builders<TEntity>.Filter.In(x => x.Id, ids);
            var deleteResult = await Collection.DeleteManyAsync(idsFilter, cancellationToken);
            return deleteResult.DeletedCount;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filter = Builders<TEntity>.Filter.Empty;
            var findResult = await Collection.FindAsync(filter, null, cancellationToken);
            return await findResult.ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            var findResult = await Collection.FindAsync(idFilter, null, cancellationToken);
            return await findResult.SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idsFilter = Builders<TEntity>.Filter.In(x => x.Id, ids);
            var findResult = await Collection.FindAsync(idsFilter, null, cancellationToken);
            return await findResult.ToListAsync(cancellationToken);
        }

        public virtual async Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Collection.InsertOneAsync(entity, null, cancellationToken);
        }

        public virtual async Task InsertMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Collection.InsertManyAsync(entities, null, cancellationToken);
        }

        public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var idFilter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            return await Collection.FindOneAndReplaceAsync(idFilter, entity, null, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var findResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
            return await findResult.ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> AsQueryable() => Collection.AsQueryable();

    }

    public class MongoEntityRepository<TEntity> : MongoEntityRepository<Guid, TEntity>, IEntityRepository<TEntity>
        where TEntity : IEntity
    {
        public MongoEntityRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
