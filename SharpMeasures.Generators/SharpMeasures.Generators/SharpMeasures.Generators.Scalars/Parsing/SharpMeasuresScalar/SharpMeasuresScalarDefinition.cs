namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class SharpMeasuresScalarDefinition : AAttributeDefinition<SharpMeasuresScalarLocations>, IScalar
{
    public IUnresolvedUnitType Unit { get; }
    public IUnresolvedVectorGroupType? VectorGroup { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IUnresolvedScalarType Difference { get; }
    IUnresolvedQuantityType IQuantity.Difference => Difference;

    public IUnresolvedUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public IUnresolvedScalarType? Reciprocal { get; }
    public IUnresolvedScalarType? Square { get; }
    public IUnresolvedScalarType? Cube { get; }
    public IUnresolvedScalarType? SquareRoot { get; }
    public IUnresolvedScalarType? CubeRoot { get; }

    public bool? GenerateDocumentation { get; }

    public SharpMeasuresScalarDefinition(IUnresolvedUnitType unit, IUnresolvedVectorGroupType? vectorGroup, bool useUnitBias, bool implementSum, bool implementDifference,
        IUnresolvedScalarType difference, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, IUnresolvedScalarType? reciprocal,
        IUnresolvedScalarType? square, IUnresolvedScalarType? cube, IUnresolvedScalarType? squareRoot, IUnresolvedScalarType? cubeRoot, bool? generateDocumentation,
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
