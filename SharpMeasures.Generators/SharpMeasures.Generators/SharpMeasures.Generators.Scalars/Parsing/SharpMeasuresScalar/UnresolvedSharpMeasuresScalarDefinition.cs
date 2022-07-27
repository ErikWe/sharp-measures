namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

internal record class UnresolvedSharpMeasuresScalarDefinition : AAttributeDefinition<SharpMeasuresScalarLocations>, IUnresolvedScalarBase
{
    public NamedType Unit { get; }
    public NamedType? VectorGroup { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    bool? IUnresolvedQuantity.ImplementSum => ImplementSum;
    public bool ImplementDifference { get; }
    bool? IUnresolvedQuantity.ImplementDifference => ImplementDifference;

    public NamedType Difference { get; }
    NamedType? IUnresolvedQuantity.Difference => Difference;

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public bool? GenerateDocumentation { get; }

    public UnresolvedSharpMeasuresScalarDefinition(NamedType unit, NamedType? vectorGroup, bool useUnitBias, bool implementSum, bool implementDifference, NamedType difference,
        string? defaultUnitName, string? defaultUnitSymbol, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot,
        bool? generateDocumentation, SharpMeasuresScalarLocations locations)
        : base(locations)
    {
        Unit = unit;
        VectorGroup = vectorGroup;

        UseUnitBias = useUnitBias;

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
