namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal class DimensionalEquivalenceExtractor : AAttributeParser<DimensionalEquivalenceDefinition, DimensionalEquivalenceAttribute>
{
    public static DimensionalEquivalenceExtractor ExtractForScalar(INamedTypeSymbol typeSymbol)
    {
        DimensionalEquivalenceExtractor extractor = new(ScalarDimensionalEquivalenceValidator.Validator);
        extractor.ExtractFirstOccurrenceFromSymbol(typeSymbol);
        return extractor;
    }

    public static DimensionalEquivalenceExtractor ExtractForVector(INamedTypeSymbol typeSymbol)
    {
        DimensionalEquivalenceExtractor extractor = new(VectorDimensionalEquivalenceValidator.Validator);
        extractor.ExtractFirstOccurrenceFromSymbol(typeSymbol);
        return extractor;
    }

    private DimensionalEquivalenceExtractor(IValidator<DimensionalEquivalenceDefinition> validator) : base(DimensionalEquivalenceParser.Parser, validator) { }
}
