namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

internal record class UnresolvedSpecializedSharpMeasuresScalarDefinition : AAttributeDefinition<SpecializedSharpMeasuresScalarLocations>, IUnresolvedSpecializedScalar
{
    public NamedType OriginalScalar { get; }
    NamedType IUnresolvedSpecializedQuantity.OriginalQuantity => OriginalScalar;

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConvertibleScalars { get; }
    bool IUnresolvedSpecializedQuantity.InheritConvertibleQuantities => InheritConvertibleScalars;
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

    public UnresolvedSpecializedSharpMeasuresScalarDefinition(NamedType originalScalar, bool inheritDerivations, bool inheritConstants, bool inheritConvertibleScalars,
        bool inheritBases, bool inheritUnits, NamedType? vector, bool? implementSum, bool? implementDifference, NamedType? difference, string? defaultUnitName,
        string? defaultUnitSymbol, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot, bool? generateDocumentation,
        SpecializedSharpMeasuresScalarLocations locations)
        : base(locations)
    {
        OriginalScalar = originalScalar;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConvertibleScalars = inheritConvertibleScalars;
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
