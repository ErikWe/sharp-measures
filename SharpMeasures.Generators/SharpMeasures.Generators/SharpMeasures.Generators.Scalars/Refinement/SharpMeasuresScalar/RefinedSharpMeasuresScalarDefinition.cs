namespace SharpMeasures.Generators.Scalars.Refinement.SharpMeasuresScalar;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal readonly record struct RefinedSharpMeasuresScalarDefinition
{
    public IUnitType Unit { get; }
    public ResizedGroup? VectorGroup { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IScalarType Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public IScalarType? Reciprocal { get; }
    public IScalarType? Square { get; }
    public IScalarType? Cube { get; }
    public IScalarType? SquareRoot { get; }
    public IScalarType? CubeRoot { get; }

    public bool GenerateDocumentation { get; }

    public RefinedSharpMeasuresScalarDefinition(IUnitType unit, ResizedGroup? vectorGroup, bool biased, bool implementSum, bool implementDifference,
        IScalarType difference, string? defaultUnitName, string? defaultUnitSymbol, IScalarType? reciprocal, IScalarType? square, IScalarType? cube,
        IScalarType? squareRoot, IScalarType? cubeRoot, bool generateDocumentation)
    {
        Unit = unit;
        VectorGroup = vectorGroup;

        UseUnitBias = biased;

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
