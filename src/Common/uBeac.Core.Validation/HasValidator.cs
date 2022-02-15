namespace uBeac;

public abstract class HasValidator<TObject> : IHasValidator<TObject>
{
    protected HasValidator(List<IValidator<TObject>> validators)
    {
        Validators = validators;
    }

    public List<IValidator<TObject>> Validators { get; }

    public async Task<ValidationResult> Validate(TObject obj)
    {
        var errors = new List<ValidationError>();

        foreach (var validator in Validators)
        {
            var result = await validator.ValidateAsync(obj);
            if (!result.Succeeded) errors.AddRange(result.Errors);
        }

        return errors.Any() ? ValidationResult.Fail(errors) : ValidationResult.Success();
    }
}