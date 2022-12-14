namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

public sealed class VectorTextBuilder
{
    public static string Compose(Func<int, int, string> elementDelegate, string separator, int dimension)
    {
        StringBuilder source = new();

        IterativeBuilding.AppendEnumerable(source, components(), separator);

        return source.ToString();

        IEnumerable<string> components()
        {
            for (var i = 0; i < dimension; i++)
            {
                yield return elementDelegate(i, dimension);
            }
        }
    }

    public static string GetLowerCasedComponentName(int componentIndex, int dimension)
    {
        if (componentIndex < 0 || dimension < componentIndex)
        {
            throw new ArgumentException($"Could not retrieve the name of component {componentIndex} for dimension {dimension}", nameof(componentIndex));
        }

        if (dimension <= LowerCasedComponentNamesArray.Length)
        {
            return LowerCasedComponentNamesArray[componentIndex];
        }

        return $"x{componentIndex - LowerCasedComponentNamesArray.Length}";
    }

    public static string GetUpperCasedComponentName(int componentIndex, int dimension) => GetLowerCasedComponentName(componentIndex, dimension).ToUpperInvariant();

    private ConcurrentDictionary<int, string> CachedTexts { get; } = new();

    private Func<int, int, string> ElementDelegate { get; }
    private string Separator { get; }

    public VectorTextBuilder(string element, string separator) : this((_, _) => element, separator) { }

    public VectorTextBuilder(Func<int, int, string> elementDelegate, string separator)
    {
        ElementDelegate = elementDelegate;
        Separator = separator;
    }

    public string GetText(int dimension)
    {
        if (CachedTexts.TryGetValue(dimension, out var text))
        {
            return text;
        }

        return ComposeAndCache(dimension);
    }

    private string ComposeAndCache(int dimension)
    {
        var text = Compose(ElementDelegate, Separator, dimension);

        CachedTexts.TryAdd(dimension, text);

        return text;
    }

    private static string[] LowerCasedComponentNamesArray { get; } = new string[] { "x", "y", "z", "w" };
}
