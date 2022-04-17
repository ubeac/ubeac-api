namespace uBeac;

public static class EnumConfiguration
{
    public static IList<Type> AllowedToExpose { get; } = new List<Type>();
    public static IList<Type> NotAllowedToExpose { get; } = new List<Type>();

    public static void AllowExposing(Type type)
    {
        ThrowIfNotEnum(type);
        ThrowIfAlreadyExists(type);

        AllowedToExpose.Add(type);
    }

    public static void AllowExposing(IEnumerable<Type> types)
    {
        foreach (var type in types) AllowExposing(type);
    }

    public static void AllowExposing(params Type[] types)
        => AllowExposing(types.ToList());

    public static void BlockExposing(Type type)
    {
        ThrowIfNotEnum(type);
        ThrowIfAlreadyExists(type);

        NotAllowedToExpose.Add(type);
    }

    public static void BlockExposing(IEnumerable<Type> types)
    {
        foreach (var type in types) BlockExposing(type);
    }

    public static void BlockExposing(params Type[] types)
        => BlockExposing(types.ToList());

    private static void ThrowIfAlreadyExists(Type type)
    {
        if (AllowedToExpose.Contains(type)) throw new ArgumentException("Argument type already is allowed to expose");
        if (NotAllowedToExpose.Contains(type)) throw new ArgumentException("Argument type already is not allowed to expose");
    }

    private static void ThrowIfNotEnum(Type type)
    {
        if (!type.IsEnum) throw new ArgumentException("Argument type is not enum");
    }
}