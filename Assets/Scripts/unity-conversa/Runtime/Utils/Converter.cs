using System.Reflection;
using Conversa.Runtime.Interfaces;
using UnityEngine;

namespace Conversa.Runtime
{
    public static class Converter
    {
        public static TReturn ConvertValue<TParam,TReturn>(TParam value)
        {
            if (value is TReturn directCastedValue) return directCastedValue;

            if (value is float floatValue && floatValue.ToString() is TReturn castedValue1)
                return castedValue1;

            if (value is int intValue && intValue.ToString() is TReturn castedValue2)
                return castedValue2;

            return default;
        }

        public static T Convert<T>(IValueProperty property)
        {
            var methodInfo = typeof(Conversa.Runtime.Converter).GetMethod(nameof(ConvertValue), BindingFlags.Public | BindingFlags.Static);
            if (methodInfo == null)
            {
                Debug.LogError("Method converter not found");
                return default;
            }
            var genericMethodInfo = methodInfo.MakeGenericMethod(property.GetValueType(), typeof(T));
            var output = genericMethodInfo.Invoke(null, new[] { property.GetValueObject() });
            return output is T castedOutput ? castedOutput : default;
            
        }

        public static bool CanConvert(System.Type from, System.Type to) =>
            (from == to) ||
            (from == typeof(float) && to == typeof(string)) ||
            (from == typeof(int) && to == typeof(string));

    }
}