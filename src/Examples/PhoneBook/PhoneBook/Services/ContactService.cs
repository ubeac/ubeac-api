namespace PhoneBook;

public class ContactService : EntityService<Contact>, IContactService
{
    private readonly IUserInfo _userInfo;

    public ContactService(IContactRepository repository, IUserInfo userInfo) : base(repository)
    {
        _userInfo = userInfo;
    }

    public async Task<IEnumerable<Contact>> GetAllByUser(CancellationToken cancellationToken = default)
        => await Repository.Find(c => c.CreatedBy == _userInfo.Id, cancellationToken);
}