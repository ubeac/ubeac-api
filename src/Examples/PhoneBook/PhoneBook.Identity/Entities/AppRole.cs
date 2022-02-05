namespace PhoneBook.Identity;

public class AppRole : Role
{
    public AppRole()
    {
    }

    public AppRole(string name) : base(name)
    {
    }

    public string CustomProperty { get; set; }
}