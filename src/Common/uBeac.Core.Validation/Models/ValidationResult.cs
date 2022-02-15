namespace uBeac;

public class ValidationResult
{
    public ValidationResult()
    {
        Succeeded = true;
    }

    public ValidationResult(List<ValidationError> errors)
    {
        Succeeded = false;
        Errors = errors;
    }

    public bool Succeeded { get; private set; }
    public List<ValidationError> Errors { get; private set; } = new();

    public static ValidationResult Success() => new();
    public static ValidationResult Fail(List<ValidationError> errors) => new(errors);
}