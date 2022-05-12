namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

public readonly record struct GeneratedUnitDefinition(INamedTypeSymbol? Quantity, bool AllowBias, bool GenerateDocumentation,
    GeneratedUnitLocations Locations, GeneratedUnitParsingData ParsingData)
{
    public CacheableGeneratedUnitDefinition ToCacheable() => CacheableGeneratedUnitDefinition.Construct(this);
}