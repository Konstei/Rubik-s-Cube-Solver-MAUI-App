using System.Reflection;

namespace Rubik_s_Cube_Solver_MAUI.Extensions;

public static class DeepCopyHelper
{
    public static T DeepCopy<T>(T obj)
    {
        return (T)DeepCopy(obj!, new Dictionary<object, object>(new ReferenceEqualityComparer()));
    }

    private static object DeepCopy(object obj, IDictionary<object, object> visited)
    {
        if (obj == null)
        {
            return null!;
        }

        Type type = obj.GetType();

        // Check if the object is of a value type or a string
        if (type.IsValueType || type == typeof(string))
        {
            return obj;
        }

        // Check if the object is already copied to handle cyclic references
        if (visited.TryGetValue(obj, out object? value))
        {
            return value;
        }

        // Create a new instance of the object
        object copy = Activator.CreateInstance(type)!;
        visited[obj] = copy;

        // Copy all fields
        foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            object fieldValue = field.GetValue(obj)!;
            field.SetValue(copy, DeepCopy(fieldValue, visited));
        }

        // Copy all properties
        foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (property.CanRead && property.CanWrite)
            {
                object propertyValue = property.GetValue(obj)!;
                property.SetValue(copy, DeepCopy(propertyValue, visited));
            }
        }

        return copy;
    }

    private class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public new bool Equals(object? x, object? y)
        {
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
    }
}
