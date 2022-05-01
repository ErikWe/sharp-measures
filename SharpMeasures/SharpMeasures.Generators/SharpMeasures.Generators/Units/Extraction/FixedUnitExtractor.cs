namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class FixedUnitExtractor : AExtractor<FixedUnitParameters, FixedUnitAttribute>
{
    public static FixedUnitExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        FixedUnitExtractor extractor = new();
        extractor.ExtractAllFromSymbol(typeSymbol);
        return extractor;
    }

    private FixedUnitExtractor() : base(FixedUnitParser.Parser, FixedUnitValidator.Validator) { }
}
