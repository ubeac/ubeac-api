namespace uBeac;

public interface IHasValidator<TObject>
{
    List<IValidator<TObject>> Validators { get; }
    Task<ValidationResult> Validate(TObject obj);
}