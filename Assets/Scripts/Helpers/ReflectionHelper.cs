using System;
using System.ComponentModel;

namespace Helpers
{
    public static class ReflectionHelper
    {
        public static string GetCallerMemberName([System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            return callerMemberName;
        }

        public static string GetCustomDescription(object objEnum)
        {
            var fi = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
        }

        public static string Description(this Enum value)
        {
            return GetCustomDescription(value);
        }
    }
}
