namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;

internal class UnitAliasExtractor : AExtractor<UnitAliasParameters, UnitAliasAttribute>
{
    public static UnitAliasExtractor Extract(INamedTypeSymbol typeSymbol)
    {
        UnitAliasExtractor extractor = new(typeSymbol);
        extractor.ExtractAllFromSymbol(typeSymbol);
        return extractor;
    }

    private UnitAliasExtractor(INamedTypeSymbol typeSymbol) : base(UnitAliasParser.Parser, new UnitAliasValidator(typeSymbol)) { }
}
