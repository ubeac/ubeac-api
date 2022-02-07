using Microsoft.AspNetCore.Mvc;

namespace PhoneBook;

[CustomAuthorize]
public class ContactsController : BaseController
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    [CustomAuthorize(Roles = "ADMIN")]
    // [CustomAuthorize(UnitType = "M")]
    public async Task<IApiListResult<ContactViewModel>> All(CancellationToken cancellationToken = default)
    {
        var contacts = await _contactService.GetAll(cancellationToken);
        var contactsVm = contacts.Select(c => new ContactViewModel
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            PhoneNumber = c.PhoneNumber,
            EmailAddress = c.EmailAddress
        });
        return contactsVm.ToApiListResult();
    }

    [HttpGet]
    public async Task<IApiListResult<ContactViewModel>> AllByUser(CancellationToken cancellationToken = default)
    {
        var contacts = await _contactService.GetAllByUser(cancellationToken);
        var contactsVm = contacts.Select(c => new ContactViewModel
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            PhoneNumber = c.PhoneNumber,
            EmailAddress = c.EmailAddress
        });
        return contactsVm.ToApiListResult();
    }

    [HttpGet]
    public async Task<IApiResult<ContactViewModel>> ById([FromQuery] IdRequest request, CancellationToken cancellationToken = default)
    {
        var contact = await _contactService.GetById(request.Id, cancellationToken);
        var contactVm = new ContactViewModel
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            PhoneNumber = contact.PhoneNumber,
            EmailAddress = contact.EmailAddress
        };
        return contactVm.ToApiResult();
    }

    [HttpPost]
    public async Task<IApiResult<bool>> Insert([FromBody] InsertContactRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _contactService.Insert(new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress
            }, cancellationToken);

            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public async Task<IApiResult<bool>> Replace([FromBody] ReplaceContactRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _contactService.Insert(new Contact
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress
            }, cancellationToken);

            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }

    [HttpPost]
    public async Task<IApiResult<bool>> Delete([FromBody] IdRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _contactService.Delete(request.Id, cancellationToken);

            return true.ToApiResult();
        }
        catch (Exception ex)
        {
            return ex.ToApiResult<bool>();
        }
    }
}

public class InsertContactRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}

public class ReplaceContactRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}

public class ContactViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}