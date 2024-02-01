using System.Reflection;

public static class ReflectionExtensions
{
    public static IEnumerable<Type?> GetImplementationsOf<TInterface>(this Assembly assembly) 
        => assembly.GetTypes().GetImplementationsOf<TInterface>();

    public static IEnumerable<Type?> GetImplementationsOf<TInterface>(this IEnumerable<Type> types) 
        => types.Where(IsImplementationOf<TInterface>);

    public static bool IsImplementationOf<T>(this Type type)
        => type.IsAssignableTo(typeof(T)) && type.IsClass && type.IsAbstract is false;
}