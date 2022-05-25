namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal class IncludeUnitsExtractor : AAttributeParser<IncludeUnitsDefinition, IncludeUnitsAttribute>
{
    public static IncludeUnitsExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        IncludeUnitsExtractor extractor = new();
        extractor.ExtractFirstOccurrenceFromSymbol(typeSymbol);
        return extractor;
    }

    private IncludeUnitsExtractor() : base(IncludeUnitsParser.Parser, IncludeUnitsValidator.Validator) { }
}
