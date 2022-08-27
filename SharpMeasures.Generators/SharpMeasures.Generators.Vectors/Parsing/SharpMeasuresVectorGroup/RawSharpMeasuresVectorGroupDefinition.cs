namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class RawSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations>, IDefaultUnitDefinition
{
    public static RawSharpMeasuresVectorGroupDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;

    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorGroupDefinition Definition => this;

    IDefaultUnitLocations IDefaultUnitDefinition.DefaultUnitLocations => Locations;

    private RawSharpMeasuresVectorGroupDefinition() : base(SharpMeasuresVectorGroupLocations.Empty) { }
}
