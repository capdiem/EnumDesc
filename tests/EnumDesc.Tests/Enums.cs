using System;
using System.ComponentModel;

namespace EnumDesc.Tests
{
    public enum EveryMemberHasDesc
    {
        [Description("蓝色")]
        Blue_Desc,

        [Description("紫色")]
        Purple_Desc,

        [Description("红色")]
        Red_Desc
    }

    public enum OnlyOneMemberHasDesc
    {
        Blue_NoDesc,

        [Description("紫色")]
        Purple_Desc,

        Red_NoDesc
    }

    public enum NoMemberHasDesc
    {
        Blue_NoDesc,

        Purple_NoDesc,

        Red_NoDesc
    }

    public enum ShortEnum: short
    {
        [Description("蓝色")]
        Blue_Desc,

        [Description("紫色")]
        Purple_Desc,

        [Description("红色")]
        Red_Desc
    }

    [Flags]
    public enum FlagsEnum
    {
        [Description("One 1")]
        One = 1,
        Two = 2,
        Three = 4
    }
}