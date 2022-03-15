# EnumDesc

A library that automatically adds support for getting description of enum in c#.

When you have an enum and it has at least one member marked with the `Description` attribute, EnumDesc will automatically generate an extension method named `GetDescription` for it.

And then EnumDesc will generate an `Enum<T>` class that provides `GetDescriptionList` and `GetDescriptionDic` methods.

## Get Started

EnumDesc can be installed using the Nuget package manager or the `dotnet` CLI.

```shell
dotnet add package EnumDesc
```

## Example

For example, there is a enum named Colour:

```c#
public enum Colour
{
    [Description("Color Blue")]
    Blue,

    [Description("Color Red")]
    Red
}
```

The EnumDesc will generate a file named `EnumDesc.g.cs`:

```C#
namespace YourNamespace
{
    public static partial class ColourExtensions
    {
        public static string GetDescription(this Colour colour)
        {
            return colour switch
            {
                Colour.Blue => "Color Blue",
                Colour.Red => "Color Red",
                _ => string.Empty
            };
        }
    }

    public partial class Enum<TEnum> where TEnum : Enum
    {
        public static IEnumerable<(TEnum? value, string desc)> GetDescriptionList() => GetDescriptionList<TEnum>();

        public static IEnumerable<(TValue? value, string desc)> GetDescriptionList<TValue>(bool withAll = false, string allDesc = "All", TValue? allValue = default)
        {
            var enumType = typeof(TEnum);

            int index = 0;

            (TValue? value, string desc)[] result;

            if (enumType.Equals(typeof(Colour)))
            {
                if (withAll)
                {
                    result = new (TValue? value, string desc)[3];
                    result[index++] = (allValue, allDesc);
                }
                else
                {
                    result = new (TValue? value, string desc)[2];
                }

                result[index++] = ((TValue)(object)Colour.Blue, "Color Blue");
                result[index++] = ((TValue)(object)Colour.Red, "Color Red");
            }
            else
            {
                throw new NotSupportedException($"No member in {enumType.FullName} has Description attribute.");
            }

            return result;
        }

        public static Dictionary<TEnum, string> GetDescriptionDic() => GetDescriptionDic<TEnum>();

        public static Dictionary<TValue, string> GetDescriptionDic<TValue>(bool withAll = false, string allDesc = "All", TValue allValue = default!) where TValue : notnull
        {
            var enumType = typeof(TEnum);

            var result = new Dictionary<TValue, string>();

            if (withAll)
            {
                result.Add(allValue, allDesc);
            }

            if (enumType.Equals(typeof(Colour)))
            {
                result.Add((TValue)(object)Colour.Blue, "Color Blue");
                result.Add((TValue)(object)Colour.Red, "Color Red");
            }
            else
            {
                throw new NotSupportedException($"No member in {enumType.FullName} has Description attribute.");
            }

            return result;
        }
    }
}
```

Usage:

```c#
var blueDesc = Colour.Blue.GetDescription();
// "Color Blue"

IEnumerable<(Colour color, string desc)> descList = Enum<Colour>.GetDescriptionList();
// [(Colour.Blue, "Color Blue"), (Colour.Red, "Color Red")]

Dictionary<Colour, string> descDic = Enum<Colour>.GetDescriptionDic();
// {Colour.Blue: "Color Blue"， Colour.Red: "Color Red"}
```

## Benchmarks

### GetDescription()

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  DefaultJob : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
```

|         Method |        Mean |      Error |     StdDev |      Median |
|--------------- |------------:|-----------:|-----------:|------------:|
| EnumReflection | 849.1797 ns | 11.9885 ns | 11.2141 ns | 847.9369 ns |
|       EnumDesc |   0.0311 ns |  0.0399 ns |  0.0354 ns |   0.0217 ns |

### Enum&lt;T&gt;.GetDescriptionList()

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  DefaultJob : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
```

|         Method |        Mean |     Error |    StdDev |
|--------------- |------------:|----------:|----------:|
| EnumReflection | 2,169.89 ns | 29.367 ns | 27.470 ns |
|       EnumDesc |    42.74 ns |  0.679 ns |  0.635 ns |

### Enum&lt;T&gt;.GetDescriptionDic()

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  DefaultJob : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
```

|         Method |        Mean |     Error |    StdDev |
|--------------- |------------:|----------:|----------:|
| EnumReflection | 2,487.17 ns | 19.692 ns | 18.420 ns |
|       EnumDesc |    32.67 ns |  0.442 ns |  0.413 ns |