using System.Reflection;

namespace uBeac.Extensions.Decoration;

public class DecorationProxy<TTarget, TDecorator> : DispatchProxy
{
    public TTarget Target { get; set; }
    public TDecorator Decorator { get; set; }

    public static TTarget Decorate(TTarget target, TDecorator decorator)
    {
        var proxy = Create<TTarget, DecorationProxy<TTarget, TDecorator>>();
        (proxy as DecorationProxy<TTarget, TDecorator>)!.Target = target;
        (proxy as DecorationProxy<TTarget, TDecorator>)!.Decorator = decorator;
        return proxy;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        if (typeof(TDecorator).GetMethods().Any(method => AreMethodEquals(method, targetMethod)))
        {
            return targetMethod.Invoke(Decorator, args);
        }

        return targetMethod.Invoke(Target, args);
    }

    private static bool AreMethodEquals(MethodInfo left, MethodInfo right)
    {
        if (left.Equals(right)) return true;
        if (left.Name != right.Name) return false;
        if (left.MemberType != right.MemberType) return false;
        if (left.ReturnType != right.ReturnType) return false;

        var leftParams = left.GetParameters();
        var rightParams = right.GetParameters();

        if (leftParams.Length != rightParams.Length) return false;
        return !leftParams.Where((t, i) => t.ParameterType != rightParams[i].ParameterType).Any();
    }
}