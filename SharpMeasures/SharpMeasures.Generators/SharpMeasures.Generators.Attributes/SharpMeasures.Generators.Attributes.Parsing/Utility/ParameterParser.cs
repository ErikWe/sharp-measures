namespace SharpMeasures.Generators.Attributes.Parsing.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Collections.Immutable;

internal static class ParameterParser
{
    public static TParameters? Parse<TParameters>(AttributeData attributeData, TParameters defaults,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        (bool success, TParameters values) = ArgumentParser.Parse(attributeData, defaults, constructorParameters, namedParameters);

        return success ? values : default;
    }

    public static IEnumerable<TParameters> Parse<TParameters, TAttribute>(INamedTypeSymbol symbol, TParameters defaults,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
        => Parse(symbol.GetAttributesOfType<TAttribute>(), defaults, constructorParameters, namedParameters);

    public static IEnumerable<TParameters> Parse<TParameters>(IEnumerable<AttributeData> attributeData, TParameters defaults,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        if (attributeData is null)
        {
            yield break;
        }

        foreach (AttributeData data in attributeData)
        {
            if (data.AttributeConstructor is null)
            {
                continue;
            }

            if (Parse(data, defaults, constructorParameters, namedParameters) is TParameters parameters)
            {
                yield return parameters;
            }
        }
    }

    public static TParameters? ParseSingle<TParameters, TAttribute>(INamedTypeSymbol symbol, TParameters defaults,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
        => ParseSingle(symbol.GetAttributesOfType<TAttribute>(), defaults, constructorParameters, namedParameters);

    public static TParameters? ParseSingle<TParameters>(IEnumerable<AttributeData> attributeData, TParameters defaults,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        if (attributeData is null)
        {
            return default;
        }

        foreach (AttributeData data in attributeData)
        {
            if (Parse(data, defaults, constructorParameters, namedParameters) is TParameters parameters)
            {
                return parameters;
            }
        }

        return default;
    }

    public static IDictionary<string, int> ParseIndices<TParameters>(AttributeData attributeData,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        if (attributeData is null)
        {
            return ImmutableDictionary<string, int>.Empty;
        }

        return ArgumentIndexParser.Parse(attributeData, constructorParameters, namedParameters);
    }

    public static IEnumerable<IDictionary<string, int>> ParseIndices<TParameters, TAttribute>(INamedTypeSymbol symbol,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
        => ParseIndices(symbol.GetAttributesOfType<TAttribute>(), constructorParameters, namedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices<TParameters>(IEnumerable<AttributeData> attributeData,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        if (attributeData is null)
        {
            yield break;
        }

        foreach (AttributeData data in attributeData)
        {
            yield return ParseIndices(data, constructorParameters, namedParameters);
        }
    }

    public static IDictionary<string, int> ParseSingleIndices<TParameters, TAttribute>(INamedTypeSymbol symbol,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
        => ParseSingleIndices(symbol.GetAttributesOfType<TAttribute>(), constructorParameters, namedParameters);

    public static IDictionary<string, int> ParseSingleIndices<TParameters>(IEnumerable<AttributeData> attributeData,
        Dictionary<string, AttributeProperty<TParameters>> constructorParameters, Dictionary<string, AttributeProperty<TParameters>> namedParameters)
    {
        if (attributeData is null)
        {
            return ImmutableDictionary<string, int>.Empty;
        }

        foreach (AttributeData data in attributeData)
        {
            return ParseIndices(data, constructorParameters, namedParameters);
        }

        return ImmutableDictionary<string, int>.Empty;
    }
}
