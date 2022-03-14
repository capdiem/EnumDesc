using System.ComponentModel;
using System.Reflection;

namespace EnumDesc.Benchmarks
{
    internal class EnumReflection<TEnum> where TEnum : Enum
    {
        public static IEnumerable<(TEnum value, string desc)> GetDescriptionList()
        {
            var enumType = typeof(TEnum);

            var lstResult = Enum.GetNames(enumType).Select(name =>
            {
                var fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);

                    if (attribute == null)
                    {
                        return ((TEnum)Enum.Parse(enumType, name), name);
                    }
                    else
                    {
                        return ((TEnum)Enum.Parse(enumType, name), attribute.Description);
                    }
                }
                else
                {
                    return ((TEnum)Enum.Parse(enumType, name), name);
                }
            });

            return lstResult;
        }

        public static Dictionary<TEnum, string> GetDescriptionDic()
        {
            Dictionary<TEnum, string> keyValues = new();

            var enumType = typeof(TEnum);

            foreach (var value in Enum.GetValues(enumType))
            {
                string val = value?.ToString() ?? string.Empty;
                var fieldInfo = enumType.GetField(val);
                var attribute = fieldInfo!.GetCustomAttribute<DescriptionAttribute>(false);

                keyValues.Add((TEnum)Enum.Parse(enumType, val), attribute?.Description ?? val);
            }

            return keyValues;
        }
    }
}
