namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class GeneratedScalar : AAttributeDefinition<GeneratedScalarLocations>
{
    public NamedType Unit { get; }
    public NamedType? Vector { get; }

    public bool Biased { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public bool GenerateDocumentation { get; }

    public GeneratedScalar(NamedType unit, NamedType? vector, bool biased, string? defaultUnitName, string? defaultUnitSymbol, NamedType? reciprocal,
        NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot, bool generateDocumentation, GeneratedScalarLocations locations)
        : base(locations)
    {
        Unit = unit;
        Vector = vector;

        Biased = biased;

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
