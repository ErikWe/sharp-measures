namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class GeneratedUnitExtractor : AExtractor<GeneratedUnitDefinition, GeneratedUnitAttribute>
{
    public static GeneratedUnitExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        GeneratedUnitExtractor extractor = new();
        extractor.ExtractFirstFromSymbol(typeSymbol);
        return extractor;
    }

    private GeneratedUnitExtractor() : base(GeneratedUnitParser.Parser, GeneratedUnitValidator.Validator) { }
}
