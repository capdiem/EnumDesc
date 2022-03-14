// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EnumDesc.Benchmarks;
using System.ComponentModel;

//BenchmarkRunner.Run<GetDescriptionTask>();
BenchmarkRunner.Run<GetDescriptionListTask>();
//BenchmarkRunner.Run<GetDescriptionDicTask>();

public class GetDescriptionTask
{
    [Benchmark]
    public string EnumReflection() => EnumReflectionHelper.GetDescription(Colour.Red);

    [Benchmark]
    public string EnumDesc() => Colour.Red.GetDescription();
}

public class GetDescriptionListTask
{
    [Benchmark]
    public List<(Colour, string)> EnumReflection() => EnumReflection<Colour>.GetDescriptionList().ToList();

    [Benchmark]
    public List<(Colour, string)> EnumDesc() => Enum<Colour>.GetDescriptionList().ToList();
}

public class GetDescriptionDicTask
{
    [Benchmark]
    public Dictionary<Colour, string> EnumReflection() => EnumReflection<Colour>.GetDescriptionDic();

    [Benchmark]
    public Dictionary<Colour, string> EnumDesc() => Enum<Colour>.GetDescriptionDic();
}

public enum Colour
{
    [Description("Color Black")]
    Black,
    [Description("Color Blue")]
    Blue,
    [Description("Color Brown")]
    Brown,
    [Description("Color Cyan")]
    Cyan,
    [Description("Color Green")]
    Green,
    [Description("Color Grey")]
    Grey,
    [Description("Color Indigo")]
    Indigo,
    [Description("Color Orange")]
    Orange,
    [Description("Color Pink")]
    Pink,
    [Description("Color Purple")]
    Purple,
    [Description("Color Red")]
    Red,
    [Description("Color White")]
    White,
    [Description("Color Yellow")]
    Yellow
}

public partial class Enum2<TEnum> where TEnum : Enum
{
    public static IEnumerable<(TEnum? value, string desc)> GetDescriptionList() => GetDescriptionList();

    public static IEnumerable<(int? value, string desc)> GetDescriptionList(bool withAll = false, string allDesc = "All", int? allValue = default)
    {
        var enumType = typeof(TEnum);

        int index = 0;

        (int? value, string desc)[] result;

        if (enumType.Equals(typeof(Colour)))
        {
            if (withAll)
            {
                result = new (int? value, string desc)[14];
                result[index++] = (allValue, allDesc);
            }
            else
            {
                result = new (int? value, string desc)[13];
            }

            result[index++] = ((int)Colour.Black, "Color Black");
            result[index++] = ((int)Colour.Blue, "Color Blue");
            result[index++] = ((int)Colour.Brown, "Color Brown");
            result[index++] = ((int)Colour.Cyan, "Color Cyan");
            result[index++] = ((int)Colour.Green, "Color Green");
            result[index++] = ((int)Colour.Grey, "Color Grey");
            result[index++] = ((int)Colour.Indigo, "Color Indigo");
            result[index++] = ((int)Colour.Orange, "Color Orange");
            result[index++] = ((int)Colour.Pink, "Color Pink");
            result[index++] = ((int)Colour.Purple, "Color Purple");
            result[index++] = ((int)Colour.Red, "Color Red");
            result[index++] = ((int)Colour.White, "Color White");
            result[index++] = ((int)Colour.Yellow, "Color Yellow");
        }
        else
        {
            throw new NotSupportedException($"No member in {enumType.FullName} has Description attribute.");
        }

        return result;
    }
}