namespace PhoneBook.Identity;

public class AppUser : User
{
    public AppUser()
    {
    }

    public AppUser(string userName) : base(userName)
    {
    }

    public string CustomProperty { get; set; }
}