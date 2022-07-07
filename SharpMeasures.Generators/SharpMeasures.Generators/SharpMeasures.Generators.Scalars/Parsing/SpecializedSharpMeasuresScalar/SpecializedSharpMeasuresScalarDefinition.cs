﻿namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SpecializedSharpMeasuresScalarDefinition : AAttributeDefinition<SpecializedSharpMeasuresScalarLocations>, ISpecializedScalar
{
    public NamedType OriginalScalar { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritBases { get; }
    public bool InheritUnits { get; }

    public NamedType? Vector { get; }

    public bool? ImplementSum { get; }
    public bool? ImplementDifference { get; }

    public NamedType? Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public bool? GenerateDocumentation { get; }

    public SpecializedSharpMeasuresScalarDefinition(NamedType originalScalar, bool inheritDerivations, bool inheritConstants, bool inheritBases, bool inheritUnits,
        NamedType? vector, bool? implementSum, bool? implementDifference, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol, NamedType? reciprocal,
        NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot, bool? generateDocumentation, SpecializedSharpMeasuresScalarLocations locations)
        : base(locations)
    {
        OriginalScalar = originalScalar;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritBases = inheritBases;
        InheritUnits = inheritUnits;

        Vector = vector;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;

        GenerateDocumentation = generateDocumentation;
    }
}
