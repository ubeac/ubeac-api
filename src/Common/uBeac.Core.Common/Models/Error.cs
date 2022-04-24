namespace uBeac;

public class Error
{
    public string Code { get; set; }
    public string Description { get; set; }

    public Error(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public Error(string code, Exception exception) : this(code, exception.Message)
    {
    }

    public Error(Exception exception) : this("UNKNOWN-ERROR", exception)
    {
    }
}