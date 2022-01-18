namespace uBeac;

public class ValidationError
{
    public ValidationError()
    {
    }

    public ValidationError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string? Code { get; set; }
    public string? Message { get; set; }
}