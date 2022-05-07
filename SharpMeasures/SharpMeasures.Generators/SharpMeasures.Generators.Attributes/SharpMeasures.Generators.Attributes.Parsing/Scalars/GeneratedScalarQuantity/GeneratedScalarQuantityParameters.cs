namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

public readonly record struct GeneratedScalarQuantityParameters(INamedTypeSymbol? Unit, bool Biased, bool GenerateDocumentation,
    GeneratedScalarQuantityParsingData ParsingData);