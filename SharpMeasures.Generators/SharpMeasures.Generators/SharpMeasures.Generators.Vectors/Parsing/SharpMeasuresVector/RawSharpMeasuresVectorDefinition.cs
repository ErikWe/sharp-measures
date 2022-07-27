namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresVectorDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations>
{
    public static RawSharpMeasuresVectorDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public int Dimension { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;

    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; } = string.Empty;
    public string? DefaultUnitSymbol { get; init; } = string.Empty;

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorDefinition Definition => this;

    private RawSharpMeasuresVectorDefinition() : base(SharpMeasuresVectorLocations.Empty) { }
}
