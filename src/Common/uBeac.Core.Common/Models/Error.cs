namespace uBeac;

public class Error
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Error()
    {
    }

    public Error(Exception exception)
    {
        Code = "UNKNOWN-ERROR";
        Description = exception.Message;
    }
}