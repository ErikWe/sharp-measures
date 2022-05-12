namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct CacheableGeneratedUnitDefinition(NamedType Quantity, bool AllowBias, bool GenerateDocumentation,
    CacheableGeneratedUnitLocations Locations, GeneratedUnitParsingData ParsingData)
{
    internal static CacheableGeneratedUnitDefinition Construct(GeneratedUnitDefinition originalDefinition)
    {
        NamedType quantity = originalDefinition.Quantity is null ? NamedType.Empty : NamedType.FromSymbol(originalDefinition.Quantity);

        return new(quantity, originalDefinition.AllowBias, originalDefinition.GenerateDocumentation,
            originalDefinition.Locations.ToCacheable(), originalDefinition.ParsingData);
    }
}