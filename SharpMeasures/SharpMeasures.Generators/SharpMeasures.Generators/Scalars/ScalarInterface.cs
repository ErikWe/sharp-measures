namespace SharpMeasures.Generators.Scalars;

internal record class ScalarInterface
{
    public DefinedType ScalarType { get; }
    public NamedType UnitType { get; }

    public bool Biased { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public ScalarInterface(DefinedType scalarType, NamedType unittype, bool biased, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot,
        NamedType? cubeRoot)
    {
        ScalarType = scalarType;
        UnitType = unittype;

        Biased = biased;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;
    }
}
