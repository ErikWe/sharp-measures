namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class OffsetUnitExtractor : AExtractor<OffsetUnitParameters, OffsetUnitAttribute>
{
    public static OffsetUnitExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        OffsetUnitExtractor extractor = new(typeSymbol);
        extractor.ExtractAllFromSymbol(typeSymbol);
        return extractor;
    }

    private OffsetUnitExtractor(INamedTypeSymbol typeSymbol) : base(OffsetUnitParser.Parser, new OffsetUnitValidator(typeSymbol)) { }
}
