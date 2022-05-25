namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal class ExcludeUnitsExtractor : AAttributeParser<ExcludeUnitsDefinition, ExcludeUnitsAttribute>
{
    public static ExcludeUnitsExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        ExcludeUnitsExtractor extractor = new();
        extractor.ExtractFirstOccurrenceFromSymbol(typeSymbol);
        return extractor;
    }

    private ExcludeUnitsExtractor() : base(ExcludeUnitsParser.Parser, ExcludeUnitsValidator.Validator) { }
}
