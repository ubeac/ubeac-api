namespace uBeac.Localization;

public class LocalizationValue : Entity
{
    public string Key { get; set; } // "welcome-message"
    public string Value { get; set; } // "Welcome to uBeac!"
    public string CultureName { get; set; } // "en-US"
}
