namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSpecializedSharpMeasuresVectorGroupDefinition :
    ARawAttributeDefinition<RawSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations>
{
    public static RawSpecializedSharpMeasuresVectorGroupDefinition Empty => new();

    public NamedType? OriginalVectorGroup { get; init; }

    public bool InheritDerivations { get; init; }
    public bool InheritConstants { get; init; }
    public bool InheritConversions { get; init; }
    public bool InheritUnits { get; init; }

    public NamedType? Scalar { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }

    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresVectorGroupDefinition Definition => this;

    private RawSpecializedSharpMeasuresVectorGroupDefinition() : base(SpecializedSharpMeasuresVectorGroupLocations.Empty) { }
}
