﻿using Microsoft.CodeAnalysis;
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
    public partial class Enum<T> where T : Enum
    {{
        public static IEnumerable<(string desc, T value)> GetDescriptionList()
        {{
            var enumType = typeof(T);

            object result = Enumerable.Empty<(string desc, T value)>();
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
                result = new List<(string desc, {model.Name} value)>()
                {{");

                    foreach (var member in model.Members)
                    {
                        sb.Append($@"
                    (""{member.Description}"", {model.Name}.{member.Name}),");
                    }

                    sb.Append(@"
                };
            }");
                }

                sb.Append($@"
            else 
            {{
                throw new NotSupportedException($""No member in {{enumType.FullName}} has Description attribute."");
            }}

            return (IEnumerable<(string desc, T value)>)result;
        }}

        public static Dictionary<T, string> GetDescriptionDic()
        {{
            var enumType = typeof(T);

            object result = new Dictionary<T, string>();
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
                result = new Dictionary<{model.Name}, string>()
                {{");

                    foreach (var member in model.Members)
                    {
                        sb.Append($@"
                    [{model.Name}.{member.Name}] = ""{member.Description}"",");
                    }

                    sb.Append(@"
                };
            }");
                }

                sb.Append($@"
            else 
            {{
                throw new NotSupportedException($""No member in {{enumType.FullName}} has Description attribute."");
            }}

            return (Dictionary<T, string>)result;
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