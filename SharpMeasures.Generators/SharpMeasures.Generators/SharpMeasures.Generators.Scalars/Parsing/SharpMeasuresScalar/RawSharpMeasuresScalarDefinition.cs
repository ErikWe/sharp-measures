namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;

internal record class RawSharpMeasuresScalarDefinition : AAttributeDefinition<SharpMeasuresScalarLocations>, IRawScalarBase, IRawDefaultUnitDefinition
{
    public NamedType Unit { get; }
    public NamedType? Vector { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    bool? IRawQuantity.ImplementSum => ImplementSum;
    public bool ImplementDifference { get; }
    bool? IRawQuantity.ImplementDifference => ImplementDifference;

    public NamedType Difference { get; }
    NamedType? IRawQuantity.Difference => Difference;

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public bool? GenerateDocumentation { get; }

    IDefaultUnitLocations IRawDefaultUnitDefinition.DefaultUnitLocations => Locations;

    public RawSharpMeasuresScalarDefinition(NamedType unit, NamedType? vectorGroup, bool useUnitBias, bool implementSum, bool implementDifference, NamedType difference, string? defaultUnitName,
        string? defaultUnitSymbol, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot, bool? generateDocumentation, SharpMeasuresScalarLocations locations)
        : base(locations)
    {
        Unit = unit;
        Vector = vectorGroup;

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
