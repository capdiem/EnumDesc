using EnumDesc.Extensions;
using System.Collections.Generic;

namespace EnumDesc
{
    internal class EnumDescModel
    {
        /// <summary>
        /// The name of Enum
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The namespace where Enum is located
        /// </summary>
        public string? Namespace { get; }

        public string? UnderlyingType { get; }

        /// <summary>
        /// The members of Enum
        /// </summary>
        public List<Member> Members { get; }

        /// <summary>
        /// The name of extension class for Enum
        /// </summary>
        public string ExtensionClass => $"{Name}Extensions";

        public string Field => Name.ToCamelCase();

        public string FormattedUnderlyingType => UnderlyingType is not null ? $"({UnderlyingType})" : "";

        public EnumDescModel(string name, string? @namespace, string? underlyingType)
        {
            Name = name;
            Namespace = @namespace;
            UnderlyingType = underlyingType;
            Members = new List<Member>();
        }

        public class Member
        {
            /// <summary>
            /// The name of member
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// The description of <see cref="System.ComponentModel.DescriptionAttribute"/>
            /// </summary>
            public string? Description { get; }

            public Member(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }
    }
}