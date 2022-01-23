namespace uBeac.Identity;


// TODO: do we really need the Identifier attribute?

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class IdentifierAttribute : Attribute
{
}