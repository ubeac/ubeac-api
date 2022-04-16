namespace API;

public class AppUser : User, IAuditEntity
{
    public AppUser()
    {
    }

    public AppUser(string userName) : base(userName)
    {
    }

    public string CreatedBy { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LastUpdatedBy { get; set; }
    public string LastUpdatedByIp { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public IApplicationContext Context { get; set; }
}