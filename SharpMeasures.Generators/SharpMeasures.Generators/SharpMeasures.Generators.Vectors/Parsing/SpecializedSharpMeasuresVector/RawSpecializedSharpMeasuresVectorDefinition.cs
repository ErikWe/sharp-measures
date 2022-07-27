namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSpecializedSharpMeasuresVectorDefinition :
    ARawAttributeDefinition<RawSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations>
{
    public static RawSpecializedSharpMeasuresVectorDefinition Empty => new();

    public NamedType? OriginalVector { get; init; }

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

    protected override RawSpecializedSharpMeasuresVectorDefinition Definition => this;

    private RawSpecializedSharpMeasuresVectorDefinition() : base(SpecializedSharpMeasuresVectorLocations.Empty) { }
}
