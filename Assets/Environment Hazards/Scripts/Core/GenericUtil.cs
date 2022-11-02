using UnityEngine;

public static class GenericUtil<T>
{
    public static T GetOrDefault(T value, string name)
    {
        if (value != null)
        {
            return value;
        }

        Debug.LogWarning(typeof(T) + " not implemented on " + name);

        return default;
    }
}
