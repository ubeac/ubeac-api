namespace PhoneBook;

public interface IContactService : IEntityService<Contact>
{
    Task<IEnumerable<Contact>> GetAllByUser(CancellationToken cancellationToken = default);
}