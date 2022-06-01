namespace SharpMeasures.Generators.Scalars.Processing;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

internal readonly record struct ProcessedGeneratedScalar
{
    public UnitInterface Unit { get; }
    public VectorCollection Vectors { get; }

    public bool Biased { get; }

    public string? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public ScalarInterface? Reciprocal { get; }
    public ScalarInterface? Square { get; }
    public ScalarInterface? Cube { get; }
    public ScalarInterface? SquareRoot { get; }
    public ScalarInterface? CubeRoot { get; }

    public ProcessedGeneratedScalar(UnitInterface unit, VectorCollection vectors, bool biased, string? defaultUnit, string? defaultUnitSymbol,
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
