using uBeac.Identity;

namespace Example2.Models
{
    public class AppRole: Role<string>
    {
        public string? MoreDescription { get; set; }
    }
}
