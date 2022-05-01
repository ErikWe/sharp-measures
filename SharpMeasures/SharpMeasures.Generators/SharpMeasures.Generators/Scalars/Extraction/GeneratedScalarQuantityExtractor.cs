namespace SharpMeasures.Generators.Scalars.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;

internal class GeneratedScalarQuantityExtractor : AExtractor<GeneratedScalarQuantityParameters, GeneratedScalarQuantityAttribute>
{
    public static GeneratedScalarQuantityExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        GeneratedScalarQuantityExtractor extractor = new();
        extractor.ExtractFirstFromSymbol(typeSymbol);
        return extractor;
    }

    private GeneratedScalarQuantityExtractor() : base(GeneratedScalarQuantityParser.Parser, GeneratedScalarQuantityValidator.Validator) { }
}
