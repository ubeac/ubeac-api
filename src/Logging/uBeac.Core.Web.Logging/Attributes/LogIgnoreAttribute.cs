namespace uBeac;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class LogIgnoreAttribute : Attribute
{
}