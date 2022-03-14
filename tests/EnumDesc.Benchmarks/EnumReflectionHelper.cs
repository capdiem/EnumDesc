using System.ComponentModel;

namespace EnumDesc.Benchmarks;

internal static class EnumReflectionHelper
{
    public static string GetDescription(this Enum enumSubitem)
    {
        string value = enumSubitem.ToString();

        var fieldInfo = enumSubitem.GetType().GetField(value);

        if (fieldInfo != null)
        {
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes == null || attributes.Length == 0)
            {
                return value;
            }
            else
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }
        }
        else
        {
            return value;
        }
    }
}
