namespace PhoneBook;

public class ContactService : EntityService<Contact>, IContactService
{
    private readonly IUserInfo _userInfo;

    public ContactService(IContactRepository repository, IUserInfo userInfo) : base(repository)
    {
        _userInfo = userInfo;
    }

    public override async Task Insert(Contact entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedBy = _userInfo.Id;
        await base.Insert(entity, cancellationToken);
    }

    public async Task<IEnumerable<Contact>> GetAllByUser(CancellationToken cancellationToken = default)
        => await Repository.Find(c => c.CreatedBy == _userInfo.Id, cancellationToken);
}