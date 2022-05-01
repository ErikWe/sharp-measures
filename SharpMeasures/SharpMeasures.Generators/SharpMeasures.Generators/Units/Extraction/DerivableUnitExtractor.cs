namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class DerivableUnitExtractor : AExtractor<DerivableUnitParameters, DerivableUnitAttribute>
{
    public static DerivableUnitExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        DerivableUnitExtractor extractor = new();
        extractor.ExtractAllFromSymbol(typeSymbol);
        return extractor;
    }

    private DerivableUnitExtractor() : base(DerivableUnitParser.Parser, DerivableUnitValidator.Validator) { }
}
