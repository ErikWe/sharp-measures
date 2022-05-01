namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using System.Collections.ObjectModel;

public readonly record struct CubableQuantityParameters(INamedTypeSymbol? Quantity, ReadOnlyCollection<INamedTypeSymbol> SecondaryQuantities);