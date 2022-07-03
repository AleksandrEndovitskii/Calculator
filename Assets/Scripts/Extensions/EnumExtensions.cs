using System;

public static class EnumExtensions
{
    public static T GetNextValue<T>(this T enumValue) where T : Enum
    {
        var values = (T[])Enum.GetValues(enumValue.GetType());
        var currentIndex = Array.IndexOf(values, enumValue);
        var nextIndex = currentIndex + 1;

        if (nextIndex >= values.Length)
        {
            var firstValue = values[0];
            return firstValue;
        }

        var nextValue = values[nextIndex];
        return nextValue;
    }
}
