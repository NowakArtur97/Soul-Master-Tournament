using System.Collections.Generic;
using UnityEngine;

public static class GenericUtil<T>
{
    private static List<string> _messages = new List<string>();

    public static T GetOrDefault(T value, string name)
    {
        if (value != null)
        {
            return value;
        }
        string message = typeof(T) + " not implemented on " + name;
        _messages.Add(message);

        if (!_messages.Contains(message))
        {
            Debug.LogError(message);
        }

        return default;
    }
}
