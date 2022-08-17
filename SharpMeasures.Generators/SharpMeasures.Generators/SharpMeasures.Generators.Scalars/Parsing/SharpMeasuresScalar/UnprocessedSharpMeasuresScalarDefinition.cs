namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class UnprocessedSharpMeasuresScalarDefinition : AUnprocessedAttributeDefinition<UnprocessedSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations>, IRawDefaultUnitDefinition
{
    public static UnprocessedSharpMeasuresScalarDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Vector { get; init; }

    public bool UseUnitBias { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;

    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public NamedType? Reciprocal { get; init; }
    public NamedType? Square { get; init; }
    public NamedType? Cube { get; init; }
    public NamedType? SquareRoot { get; init; }
    public NamedType? CubeRoot { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override UnprocessedSharpMeasuresScalarDefinition Definition => this;

    IDefaultUnitLocations IRawDefaultUnitDefinition.DefaultUnitLocations => Locations;

    private UnprocessedSharpMeasuresScalarDefinition() : base(SharpMeasuresScalarLocations.Empty) { }
}
