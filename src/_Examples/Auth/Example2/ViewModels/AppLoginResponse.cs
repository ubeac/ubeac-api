using uBeac.Web.Identity;

namespace Example2.ViewModels
{
    public class AppLoginResponse : LoginResponse<int>
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
