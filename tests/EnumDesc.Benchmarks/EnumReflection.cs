using System.ComponentModel;
using System.Reflection;

namespace EnumDesc.Benchmarks
{
    internal class EnumReflection<TEnum> where TEnum : Enum
    {
        public static IEnumerable<(TEnum value, string desc)> GetDescriptionList() => GetDescriptionList<TEnum>();

        public static IEnumerable<(TValue value, string desc)> GetDescriptionList<TValue>(bool withAll = false, string allDesc = "All", TValue? allValue = default)
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
                        return ((TValue)Enum.Parse(enumType, name), name);
                    }
                    else
                    {
                        return ((TValue)Enum.Parse(enumType, name), attribute.Description);
                    }
                }
                else
                {
                    return ((TValue)Enum.Parse(enumType, name), name);
                }
            }).ToList();

            if (withAll)
            {
                lstResult.Insert(0, ((TValue)Enum.Parse(enumType, allValue?.ToString() ?? string.Empty), allDesc));
            }

            return lstResult;
        }

        public static Dictionary<TEnum, string> GetDescriptionDic() => GetDescriptionDic<TEnum>();

        public static Dictionary<TValue, string> GetDescriptionDic<TValue>(bool withAll = false, string allDesc = "All", TValue allValue = default!)
            where TValue : notnull
        {
            Dictionary<TValue, string> keyValues = new Dictionary<TValue, string>();
            var enumType = typeof(TEnum);

            if (withAll)
            {
                keyValues.Add(allValue, allDesc);
            }

            foreach (var value in Enum.GetValues(enumType))
            {
                string val = value?.ToString() ?? string.Empty;
                var fieldInfo = enumType.GetField(val);
                var attribute = fieldInfo!.GetCustomAttribute<DescriptionAttribute>(false);

                keyValues.Add((TValue)Enum.Parse(enumType, val), attribute?.Description ?? val);
            }

            return keyValues;
        }
    }
}
