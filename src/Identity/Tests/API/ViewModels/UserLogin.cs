using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API;

public class LoginRequest
{
    [Required]
    [LogIgnore("-----")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [LogIgnore("*****")]
    public string Password { get; set; }

    public AltLoginRequest Nested { get; set; }
    public LoginRequest()
    {
        Nested = new AltLoginRequest();
    }
}

public class LoginResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiry { get; set; }
}

public class AltLoginRequest
{
    public int MyProperty { get; set; }
    [LogIgnore("")]
    public string MyProperty1 { get; set; }
    [LogIgnore("")]
    public decimal MyProperty2 { get; set; }
    [LogIgnore]
    public List<SecondLevel> MyProperty3 { get; set; }
}


public class SecondLevel
{
    public int MyProperty { get; set; }
    public string MyProperty1 { get; set; }
}