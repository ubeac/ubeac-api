using System.Linq.Expressions;
using System.Reflection;

namespace uBeac.Identity;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class IdentifierAttribute : Attribute
{
}