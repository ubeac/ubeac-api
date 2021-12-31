using uBeac.Web.Identity;

namespace Example2.ViewModels
{
    public class AppRegisterRequest: RegisterRequest
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
