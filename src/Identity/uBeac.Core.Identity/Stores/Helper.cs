using System.ComponentModel;

namespace uBeac.Identity
{
    public static class Helper
    {
        public static TKey GetTypedKey<TKey>(this string id)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TKey));

            if (converter == null)
                throw new ArgumentNullException($"Unable to convert string to type {typeof(TKey).FullName}");

            return (TKey)converter.ConvertFromString(id);
        }
    }
}
