namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class RawSharpMeasuresScalarDefinition : ARawAttributeDefinition<RawSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSharpMeasuresScalarDefinition FromSymbolic(SymbolicSharpMeasuresScalarDefinition symbolicDefinition) => new RawSharpMeasuresScalarDefinition(symbolicDefinition.Locations) with
    {
        Unit = symbolicDefinition.Unit?.AsNamedType(),
        Vector = symbolicDefinition.Vector?.AsNamedType(),
        UseUnitBias = symbolicDefinition.UseUnitBias,
        ImplementSum = symbolicDefinition.ImplementSum,
        ImplementDifference = symbolicDefinition.ImplementDifference,
        Difference = symbolicDefinition.Difference?.AsNamedType(),
        DefaultUnitInstanceName = symbolicDefinition.DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol = symbolicDefinition.DefaultUnitInstanceSymbol,
        Reciprocal = symbolicDefinition.Reciprocal?.AsNamedType(),
        Square = symbolicDefinition.Square?.AsNamedType(),
        Cube = symbolicDefinition.Cube?.AsNamedType(),
        SquareRoot = symbolicDefinition.SquareRoot?.AsNamedType(),
        CubeRoot = symbolicDefinition.CubeRoot?.AsNamedType(),
        GenerateDocumentation = symbolicDefinition.GenerateDocumentation
    };

    public NamedType? Unit { get; init; }
    public NamedType? Vector { get; init; }

    public bool UseUnitBias { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;
    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public NamedType? Reciprocal { get; init; }
    public NamedType? Square { get; init; }
    public NamedType? Cube { get; init; }
    public NamedType? SquareRoot { get; init; }
    public NamedType? CubeRoot { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresScalarDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSharpMeasuresScalarDefinition(SharpMeasuresScalarLocations locations) : base(locations) { }
}
