namespace SharpMeasures.Generators.Attributes.Parsing.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Collections.Immutable;

public sealed class ParameterParser<TParameters, TAttribute>
{
    private IDictionary<string, AttributeProperty<TParameters>> ConstructorParameters { get; }
    private IDictionary<string, AttributeProperty<TParameters>> NamedParameters { get; }
    private TParameters Defaults { get; }

    internal ParameterParser(Dictionary<string, AttributeProperty<TParameters>> constructorParameters,
        Dictionary<string, AttributeProperty<TParameters>> namedParameters, TParameters defaults)
    {
        ConstructorParameters = constructorParameters;
        NamedParameters = namedParameters;
        Defaults = defaults;
    }

    internal ParameterParser(IEnumerable<AttributeProperty<TParameters>> parameters, TParameters defaults)
    {
        ConstructorParameters = parameters.ToImmutableDictionary(static (property) => property.ParameterName);
        NamedParameters = parameters.ToImmutableDictionary(static (property) => property.Name);
        Defaults = defaults;
    }

    public TParameters? Parse(AttributeData attributeData)
    {
        (bool success, TParameters values) = ArgumentParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

        return success ? values : default;
    }

    public IEnumerable<TParameters> Parse(IEnumerable<AttributeData> attributeData)
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

            if (Parse(data) is TParameters parameters)
            {
                yield return parameters;
            }
        }
    }

    public TParameters? ParseSingle(INamedTypeSymbol symbol)
        => symbol.GetAttributeOfType<TAttribute>() is AttributeData attributeData ? Parse(attributeData) : default;

    public IEnumerable<TParameters> Parse(INamedTypeSymbol symbol)
        => Parse(symbol.GetAttributesOfType<TAttribute>());

    public IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => attributeData is not null
            ? ArgumentIndexParser.Parse(attributeData, ConstructorParameters, NamedParameters)
            : ImmutableDictionary<string, int>.Empty;

    public IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
    {
        if (attributeData is null)
        {
            yield break;
        }

        foreach (AttributeData data in attributeData)
        {
            yield return ParseIndices(data);
        }
    }

    public IDictionary<string, int> ParseSingleIndices(INamedTypeSymbol symbol)
        => symbol.GetAttributeOfType<TAttribute>() is AttributeData attributeData ? ParseSingleIndices(attributeData) : ImmutableDictionary<string, int>.Empty;

    public IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParseIndices(symbol.GetAttributesOfType<TAttribute>());

    public IDictionary<string, int> ParseSingleIndices(AttributeData attributeData)
        => ParseIndices(attributeData);
}
