namespace PhoneBook;

public class MongoContactRepository : MongoEntityRepository<Contact>, IContactRepository
{
    public MongoContactRepository(IMongoDBContext mongoDbContext) : base(mongoDbContext)
    {
    }
}