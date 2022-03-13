# EnumDesc

A library that automatically adds support for getting description of enum in c#.

When you have an enum and it has at least one member marked with the `Description` attribute, EnumDesc will automatically generate an extension method named `GetDescription` for it.

And then EnumDesc will generate an `Enum<T>` class that provides `GetDescriptionList` and `GetDescriptionDic` methods.

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
        public static IEnumerable<(string desc, T value)> GetDescriptionList()
        {
            var enumType = typeof(T);

            object result = Enumerable.Empty<(string desc, T value)>();

            if (enumType.Equals(typeof(Colour)))
            {
                result = new List<(string desc, Colour value)>()
                {
                    ("Color Blue", Colour.Blue),
                    ("Color Red", Colour.Red),
                };
            }
            else
            {
                throw new NotSupportedException($"No member in {enumType.FullName} has Description attribute.");
            }

            return (IEnumerable<(string desc, T value)>)result;
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