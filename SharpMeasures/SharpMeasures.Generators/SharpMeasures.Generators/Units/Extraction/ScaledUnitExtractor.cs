namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class ScaledUnitExtractor : AExtractor<ScaledUnitParameters, ScaledUnitAttribute>
{
    public static ScaledUnitExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        ScaledUnitExtractor extractor = new(typeSymbol);
        extractor.ExtractAllFromSymbol(typeSymbol);
        return extractor;
    }

    private ScaledUnitExtractor(INamedTypeSymbol typeSymbol) : base(ScaledUnitParser.Parser, new ScaledUnitValidator(typeSymbol)) { }
}
