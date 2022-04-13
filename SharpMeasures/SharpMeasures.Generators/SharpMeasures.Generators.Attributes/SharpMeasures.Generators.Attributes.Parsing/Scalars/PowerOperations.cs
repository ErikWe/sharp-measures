namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

public readonly record struct PowerOperations(InvertibleQuantityAttributeParameters? Invertible,
    SquarableQuantityAttributeParameters? Squarable, CubableQuantityAttributeParameters? Cubable,
    SquareRootableQuantityAttributeParameters? SquareRootable, CubeRootableQuantityAttributeParameters? CubeRootable)
{
    public static PowerOperations Parse(INamedTypeSymbol typeSymbol) => new(InvertibleQuantityAttributeParameters.Parse(typeSymbol),
        SquarableQuantityAttributeParameters.Parse(typeSymbol), CubableQuantityAttributeParameters.Parse(typeSymbol),
        SquareRootableQuantityAttributeParameters.Parse(typeSymbol), CubeRootableQuantityAttributeParameters.Parse(typeSymbol));
}
