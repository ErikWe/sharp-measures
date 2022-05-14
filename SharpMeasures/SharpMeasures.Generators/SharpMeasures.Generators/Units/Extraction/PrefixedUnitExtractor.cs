namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class PrefixedUnitExtractor : AExtractor<PrefixedUnitDefinition, PrefixedUnitAttribute>
{
    public static PrefixedUnitExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        PrefixedUnitExtractor extractor = new(typeSymbol);
        extractor.ExtractAllFromSymbol(typeSymbol);
        return extractor;
    }

    private PrefixedUnitExtractor(INamedTypeSymbol typeSymbol) : base(PrefixedUnitParser.Parser, new PrefixedUnitValidator(typeSymbol)) { }
}
