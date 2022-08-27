namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class RawSharpMeasuresVectorDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations>, IDefaultUnitDefinition
{
    public static RawSharpMeasuresVectorDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public int? Dimension { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;
    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorDefinition Definition => this;

    IDefaultUnitLocations IDefaultUnitDefinition.DefaultUnitLocations => Locations;

    private RawSharpMeasuresVectorDefinition() : base(SharpMeasuresVectorLocations.Empty) { }
}
