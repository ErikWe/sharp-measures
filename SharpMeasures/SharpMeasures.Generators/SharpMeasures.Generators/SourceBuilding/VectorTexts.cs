namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Text;

internal class VectorTexts
{
    public static string Compose(Func<int, int, string> elementDelegate, string separator, int dimension)
    {
        StringBuilder source = new();
        IterativeBuilding.AppendEnumerable(source, components(), separator);
        return source.ToString();

        IEnumerable<string> components()
        {
            for (int i = 0; i < dimension; i++)
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

    public static VectorTexts CommaSeparatedNames_LowerCased { get; } = new(GetLowerCasedComponentName, ", ");
    public static VectorTexts CommaSeparatedNames_UpperCased { get; } = new(GetUpperCasedComponentName, ", ");

    public static VectorTexts ComponentTupleAccess_UpperCased { get; } = new(static (i, d) => $"component.{GetUpperCasedComponentName(i, d)}", ", ");
    public static VectorTexts ComponentTupleAccess_LowerCased { get; } = new(static (i, d) => $"component.{GetLowerCasedComponentName(i, d)}", ", ");

    public static VectorTexts CommaSeparatedElements(string type, Func<int, int, string> componentNameDelegate)
    {
        return new VectorTexts(wrapper, ", ");

        string wrapper(int componentIndex, int dimension) => $"{type} {componentNameDelegate(componentIndex, dimension)}";
    }

    public static VectorTexts CommaSeparatedElements_LowerCased(string type)
    {
        return CommaSeparatedElements(type, GetLowerCasedComponentName);
    }

    public static VectorTexts CommaSeparatedElements_UpperCased(string type)
    {
        return CommaSeparatedElements(type, GetUpperCasedComponentName);
    }

    private Dictionary<int, string> CachedTexts { get; } = new();

    private Func<int, int, string> ElementDelegate { get; }
    private string Separator { get; }

    public VectorTexts(string element, string separator) : this((_, _) => element, separator) { }

    public VectorTexts(Func<int, int, string> elementDelegate, string separator)
    {
        ElementDelegate = elementDelegate;
        Separator = separator;
    }

    public string GetText(int dimension)
    {
        if (CachedTexts.TryGetValue(dimension, out string text))
        {
            return text;
        }

        text = ComposeAndCache(dimension);
        return text;
    }

    private string ComposeAndCache(int dimension)
    {
        string text = Compose(ElementDelegate, Separator, dimension);
        CachedTexts.Add(dimension, text);
        return text;
    }

    private static string[] LowerCasedComponentNamesArray { get; } = new string[] { "x", "y", "z", "w" };
}
