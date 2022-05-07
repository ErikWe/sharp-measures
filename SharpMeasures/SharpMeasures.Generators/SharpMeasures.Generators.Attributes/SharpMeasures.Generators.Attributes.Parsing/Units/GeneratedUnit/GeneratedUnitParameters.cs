namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

public readonly record struct GeneratedUnitParameters(INamedTypeSymbol? Quantity, bool AllowBias, bool GenerateDocumentation,
    GeneratedUnitParsingData ParsingData);