namespace SharpMeasures.Generators.Scalars.Refinement.SharpMeasuresScalar;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal readonly record struct RefinedSharpMeasuresScalarDefinition
{
    public UnitInterface Unit { get; }
    public ResizedGroup? VectorGroup { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public ScalarInterface Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public ScalarInterface? Reciprocal { get; }
    public ScalarInterface? Square { get; }
    public ScalarInterface? Cube { get; }
    public ScalarInterface? SquareRoot { get; }
    public ScalarInterface? CubeRoot { get; }

    public bool GenerateDocumentation { get; }

    public RefinedSharpMeasuresScalarDefinition(UnitInterface unit, ResizedGroup? vectorGroup, bool biased, bool implementSum, bool implementDifference,
        ScalarInterface difference, string? defaultUnitName, string? defaultUnitSymbol, ScalarInterface? reciprocal, ScalarInterface? square, ScalarInterface? cube,
        ScalarInterface? squareRoot, ScalarInterface? cubeRoot, bool generateDocumentation)
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
