using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumDesc
{
    internal static class EnumDescHelper
    {
        internal static List<(string hintName, string sourceText)> GenerateSources(IEnumerable<EnumDescModel> models)
        {
            var sources = new List<(string hintName, string sourceText)>();

            var groups = models.GroupBy(m => m.Namespace).ToList();

            var multiNamespacesExists = groups.Count() > 1;

            foreach (var item in groups)
            {
                var sb = new StringBuilder(@"using System;
using System.Collections.Generic;
using System.Linq;
");

                if (item.Key is not null)
                {
                    sb.Append($@"
namespace {item.Key}
{{");
                }

                foreach (var model in item)
                {
                    sb.Append($@"
    public static partial class {model.ExtensionClass}
    {{
        public static string GetDescription(this {model.Name} {model.Field})
        {{
            return {model.Field} switch
            {{");

                    foreach (var member in model.Members)
                    {
                        sb.Append($@"
                {model.Name}.{member.Name} => ""{member.Description}"",");
                    }

                    sb.Append(@"
                _ => string.Empty
            };
        }
    }
");
                }

                sb.Append($@"
    public partial class Enum<TEnum> where TEnum : Enum
    {{
        public static IEnumerable<(TEnum? value, string desc)> GetDescriptionList() => GetDescriptionList<TEnum>();

        public static IEnumerable<(TValue? value, string desc)> GetDescriptionList<TValue>(bool withAll = false, string allDesc = ""All"", TValue? allValue = default)
        {{
            var enumType = typeof(TEnum);

            int index = 0;

            (TValue? value, string desc)[] result;
");

                foreach (var (model, index) in item.Select((m, i) => (m, i)))
                {
                    if (index == 0)
                    {
                        sb.Append($@"
            if (enumType.Equals(typeof({model.Name})))");
                    }
                    else
                    {
                        sb.Append($@"
            else if (enumType.Equals(typeof({model.Name})))");
                    }

                    sb.Append($@"
            {{
                if (withAll)
                {{
                    result = new (TValue? value, string desc)[{model.Members.Count + 1}];
                    result[index++] = (allValue, allDesc);
                }}
                else
                {{
                    result = new (TValue? value, string desc)[{model.Members.Count}];
                }}
");

                    foreach (var member in model.Members)
                    {
                        sb.Append($@"
                result[index++] = ((TValue)(object){model.FormattedUnderlyingType}{model.Name}.{member.Name}, ""{member.Description}"");");
                    }

                    sb.Append(@"
            }");
                }

                sb.Append($@"
            else 
            {{
                throw new NotSupportedException($""No member in {{enumType.FullName}} has Description attribute."");
            }}

            return result;
        }}

        public static Dictionary<TEnum, string> GetDescriptionDic() => GetDescriptionDic<TEnum>();

        public static Dictionary<TValue, string> GetDescriptionDic<TValue>(bool withAll = false, string allDesc = ""All"", TValue allValue = default!) where TValue : notnull
        {{
            var enumType = typeof(TEnum);

            var result = new Dictionary<TValue, string>();

            if (withAll)
            {{
                result.Add(allValue, allDesc);
            }}
");

                foreach (var (model, index) in item.Select((m, i) => (m, i)))
                {
                    if (index == 0)
                    {
                        sb.Append($@"
            if (enumType.Equals(typeof({model.Name})))");
                    }
                    else
                    {
                        sb.Append($@"
            else if (enumType.Equals(typeof({model.Name})))");
                    }

                    sb.Append($@"
            {{");
                    foreach (var member in model.Members)
                    {
                        sb.Append($@"
                result.Add((TValue)(object){model.FormattedUnderlyingType}{model.Name}.{member.Name}, ""{member.Description}"");");
                    }

                    sb.Append(@"
            }");
                }

                sb.Append($@"
            else 
            {{
                throw new NotSupportedException($""No member in {{enumType.FullName}} has Description attribute."");
            }}

            return result;
        }}
    }}
        ");

                if (item.Key is not null)
                {
                    sb.Append(@"
}");
                }

                var sourceText = sb.ToString();

                sources.Add((
                    multiNamespacesExists ? $"EnumDesc_{item.Key}.g.cs" : "EnumDesc.g.cs",
                    sourceText
                ));
            }

            return sources;
        }
    }
}