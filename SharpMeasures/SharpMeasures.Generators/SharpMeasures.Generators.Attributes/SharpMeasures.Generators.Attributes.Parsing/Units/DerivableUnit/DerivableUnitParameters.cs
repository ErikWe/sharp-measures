namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.ObjectModel;

public readonly record struct DerivableUnitParameters(ReadOnlyCollection<INamedTypeSymbol> Signature, ReadOnlyCollection<INamedTypeSymbol> Quantities,
    string Expression, DerivableUnitParsingData ParsingData);