namespace uBeac.Identity;

public class InsertUser
{
    public virtual string UserName { get; set; }
    public virtual string Password { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual bool PhoneNumberConfirmed { get; set; }
    public virtual string Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
}