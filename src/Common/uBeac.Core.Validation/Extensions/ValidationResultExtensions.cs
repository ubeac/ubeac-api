namespace uBeac.Extensions;

public static class ValidationResultExtensions
{
    public static void ThrowIfInvalid(this ValidationResult validationResult)
    {
        if (!validationResult.Succeeded)
        {
            var message = string.Empty;
            throw new Exception(string.Join("\r\n", validationResult.Errors.Select(x => x.Code + "," + x.Message)));
        }
    }

    public static ValidationResult CreateResult(this List<ValidationError> errors) =>
        errors.Any() ? new ValidationResult(errors) : new ValidationResult();
}