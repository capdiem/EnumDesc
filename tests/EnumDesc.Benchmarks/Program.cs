﻿// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EnumDesc.Benchmarks;
using System.ComponentModel;

BenchmarkRunner.Run<GetDescriptionTask>();
BenchmarkRunner.Run<GetDescriptionListTask>();
BenchmarkRunner.Run<GetDescriptionDicTask>();

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

