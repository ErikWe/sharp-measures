namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

public record class DerivedUnitDefinition(string Name, string Plural, IReadOnlyList<INamedTypeSymbol> Signature,
    IReadOnlyList<string> Units, DerivedUnitLocations Locations, DerivedUnitParsingData ParsingData)
    : IUnitDefinition
{
    IUnitLocations IUnitDefinition.Locations => Locations;

    public CacheableDerivedUnitDefinition ToCacheable() => CacheableDerivedUnitDefinition.Construct(this);

    internal DerivedUnitDefinition ParseSignature(INamedTypeSymbol[] signature)
    {
        return this with
        {
            Signature = signature,
            ParsingData = ParsingData with { SignatureCouldBeParsed = true }
        };
    }
}