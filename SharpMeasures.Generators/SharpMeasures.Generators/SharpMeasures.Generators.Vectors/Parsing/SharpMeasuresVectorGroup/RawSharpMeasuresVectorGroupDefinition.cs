namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations>
{
    public static RawSharpMeasuresVectorGroupDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;

    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; } = string.Empty;
    public string? DefaultUnitSymbol { get; init; } = string.Empty;

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorGroupDefinition Definition => this;

    private RawSharpMeasuresVectorGroupDefinition() : base(SharpMeasuresVectorGroupLocations.Empty) { }
}
