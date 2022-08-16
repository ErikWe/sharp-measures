namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresVectorGroupDefinition : AUnprocessedAttributeDefinition<RawSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations>
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

    private RawSharpMeasuresVectorGroupDefinition() : base(SharpMeasuresVectorGroupLocations.Empty) { }
}
