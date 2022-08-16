namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;

internal record class SharpMeasuresScalarDefinition : AAttributeDefinition<SharpMeasuresScalarLocations>, IScalar
{
    public IRawUnitType Unit { get; }
    public IRawVectorGroupType? VectorGroup { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IRawScalarType Difference { get; }
    IRawQuantityType IQuantity.Difference => Difference;

    public IRawUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public IRawScalarType? Reciprocal { get; }
    public IRawScalarType? Square { get; }
    public IRawScalarType? Cube { get; }
    public IRawScalarType? SquareRoot { get; }
    public IRawScalarType? CubeRoot { get; }

    public bool? GenerateDocumentation { get; }

    public SharpMeasuresScalarDefinition(IRawUnitType unit, IRawVectorGroupType? vectorGroup, bool useUnitBias, bool implementSum, bool implementDifference,
        IRawScalarType difference, IRawUnitInstance? defaultUnit, string? defaultUnitSymbol, IRawScalarType? reciprocal,
        IRawScalarType? square, IRawScalarType? cube, IRawScalarType? squareRoot, IRawScalarType? cubeRoot, bool? generateDocumentation,
        SharpMeasuresScalarLocations locations)
        : base(locations)
    {
        Unit = unit;
        VectorGroup = vectorGroup;

        UseUnitBias = useUnitBias;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;

        GenerateDocumentation = generateDocumentation;
    }
}
