namespace uBeac;

public interface IValidator<in TObject>
{
    Task<ValidationResult> ValidateAsync(TObject obj);
}