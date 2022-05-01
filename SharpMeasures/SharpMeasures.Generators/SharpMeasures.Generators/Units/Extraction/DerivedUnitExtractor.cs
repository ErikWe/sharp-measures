namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class DerivedUnitExtractor : AExtractor<DerivedUnitParameters, DerivedUnitAttribute>
{
    public static DerivedUnitExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        DerivedUnitExtractor extractor = new(typeSymbol);
        extractor.ExtractAllFromSymbol(typeSymbol);
        return extractor;
    }

    private DerivedUnitExtractor(INamedTypeSymbol typeSymbol) : base(DerivedUnitParser.Parser, new DerivedUnitValidator(typeSymbol)) { }
}
