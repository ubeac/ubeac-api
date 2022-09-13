namespace Identity.EntityFramework.API.ViewModels
{
    public class UserRoles
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }        
    }

    public class CurrentUserWithRolesModel
    {
        public Guid Id { get; set; }
    }
}
