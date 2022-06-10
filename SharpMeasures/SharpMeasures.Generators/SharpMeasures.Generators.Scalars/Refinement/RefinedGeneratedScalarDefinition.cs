namespace SharpMeasures.Generators.Scalars.Refinement;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal readonly record struct RefinedGeneratedScalarDefinition
{
    public UnitInterface Unit { get; }
    public ResizedVectorGroup? Vectors { get; }

    public bool Biased { get; }

    public string? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public ScalarInterface? Reciprocal { get; }
    public ScalarInterface? Square { get; }
    public ScalarInterface? Cube { get; }
    public ScalarInterface? SquareRoot { get; }
    public ScalarInterface? CubeRoot { get; }

    public RefinedGeneratedScalarDefinition(UnitInterface unit, ResizedVectorGroup? vectors, bool biased, string? defaultUnit, string? defaultUnitSymbol,
        ScalarInterface? reciprocal, ScalarInterface? square, ScalarInterface? cube, ScalarInterface? squareRoot, ScalarInterface? cubeRoot)
    {
        Unit = unit;
        Vectors = vectors;

        Biased = biased;
        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;
    }
}
