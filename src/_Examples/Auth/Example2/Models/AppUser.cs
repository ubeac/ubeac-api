using uBeac.Identity;

namespace Example2.Models
{
    public class AppUser : User<int>
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
