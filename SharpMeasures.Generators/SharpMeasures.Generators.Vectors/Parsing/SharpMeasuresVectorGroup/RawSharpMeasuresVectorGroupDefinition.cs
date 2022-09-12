namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class RawSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSharpMeasuresVectorGroupDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;

    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorGroupDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSharpMeasuresVectorGroupDefinition() : base(SharpMeasuresVectorGroupLocations.Empty) { }
}
