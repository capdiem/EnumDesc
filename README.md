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

    public partial class Enum<T> where T : Enum
    {
        public static IEnumerable<(T value, string desc)> GetDescriptionList()
        {
            var enumType = typeof(T);

            object result = Enumerable.Empty<(T value, string desc)>();

            if (enumType.Equals(typeof(Colour)))
            {
                result = new List<(string desc, Colour value)>()
                {
                    (Colour.Blue, "Color Blue"),
                    (Colour.Red, "Color Red"),
                };
            }
            else
            {
                throw new NotSupportedException($"No member in {enumType.FullName} has Description attribute.");
            }

            return (IEnumerable<(T value, string desc)>)result;
        }

        public static Dictionary<T, string> GetDescriptionDic()
        {
            var enumType = typeof(T);

            object result = new Dictionary<T, string>();

            if (enumType.Equals(typeof(Colour)))
            {
                result = new Dictionary<Colour, string>()
                {
                    [Colour.Blue] = "Color Blue",
                    [Colour.Red] = "Color Red",
                };
            }
            else
            {
                throw new NotSupportedException($"No member in {enumType.FullName} has Description attribute.");
            }

            return (Dictionary<T, string>)result;
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