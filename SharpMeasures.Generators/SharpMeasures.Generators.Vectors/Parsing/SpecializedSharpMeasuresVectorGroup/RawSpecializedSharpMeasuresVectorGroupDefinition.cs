namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class RawSpecializedSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<RawSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations>, IDefaultUnitDefinition
{
    public static RawSpecializedSharpMeasuresVectorGroupDefinition Empty => new();

    public NamedType? OriginalVectorGroup { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public NamedType? Scalar { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresVectorGroupDefinition Definition => this;

    IDefaultUnitLocations IDefaultUnitDefinition.DefaultUnitLocations => Locations;

    private RawSpecializedSharpMeasuresVectorGroupDefinition() : base(SpecializedSharpMeasuresVectorGroupLocations.Empty) { }
}
