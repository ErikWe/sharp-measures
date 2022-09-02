namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class RawSharpMeasuresVectorDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSharpMeasuresVectorDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public int? Dimension { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;
    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSharpMeasuresVectorDefinition() : base(SharpMeasuresVectorLocations.Empty) { }
}
