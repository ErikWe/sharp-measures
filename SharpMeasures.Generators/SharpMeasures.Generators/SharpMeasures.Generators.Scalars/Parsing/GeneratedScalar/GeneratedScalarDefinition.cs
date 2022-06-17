namespace SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedScalarDefinition : AAttributeDefinition<GeneratedScalarLocations>
{
    public NamedType Unit { get; }
    public NamedType? Vector { get; }

    public bool UseUnitBias { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public NamedType? Reciprocal { get; }
    public NamedType? Square { get; }
    public NamedType? Cube { get; }
    public NamedType? SquareRoot { get; }
    public NamedType? CubeRoot { get; }

    public bool GenerateDocumentation { get; }

    public GeneratedScalarDefinition(NamedType unit, NamedType? vector, bool useUnitBias, bool implementSum, bool implementDifference, NamedType difference,
        string? defaultUnitName, string? defaultUnitSymbol, NamedType? reciprocal, NamedType? square, NamedType? cube, NamedType? squareRoot, NamedType? cubeRoot,
        bool generateDocumentation, GeneratedScalarLocations locations)
        : base(locations)
    {
        Unit = unit;
        Vector = vector;

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
