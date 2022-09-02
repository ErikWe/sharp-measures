namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class RawSharpMeasuresScalarDefinition : ARawAttributeDefinition<RawSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSharpMeasuresScalarDefinition Empty => new();

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

    private RawSharpMeasuresScalarDefinition() : base(SharpMeasuresScalarLocations.Empty) { }
}
