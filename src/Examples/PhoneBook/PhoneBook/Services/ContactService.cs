namespace PhoneBook;

public class ContactService : EntityService<Contact>, IContactService
{
    public ContactService(IContactRepository repository) : base(repository)
    {
    }
}