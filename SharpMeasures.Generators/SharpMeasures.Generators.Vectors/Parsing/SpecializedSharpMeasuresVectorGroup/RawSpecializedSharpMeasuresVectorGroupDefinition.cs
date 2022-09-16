namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class RawSpecializedSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<RawSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSpecializedSharpMeasuresVectorGroupDefinition FromSymbolic(SymbolicSpecializedSharpMeasuresVectorGroupDefinition symbolicDefinition) => new RawSpecializedSharpMeasuresVectorGroupDefinition(symbolicDefinition.Locations) with
    {
        OriginalQuantity = symbolicDefinition.OriginalQuantity?.AsNamedType(),
        InheritDerivations = symbolicDefinition.InheritDerivations,
        InheritConstants = symbolicDefinition.InheritConstants,
        InheritConversions = symbolicDefinition.InheritConversions,
        InheritUnits = symbolicDefinition.InheritUnits,
        Scalar = symbolicDefinition.Scalar?.AsNamedType(),
        ImplementSum = symbolicDefinition.ImplementSum,
        ImplementDifference = symbolicDefinition.ImplementDifference,
        Difference = symbolicDefinition.Difference?.AsNamedType(),
        DefaultUnitInstanceName = symbolicDefinition.DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol = symbolicDefinition.DefaultUnitInstanceSymbol,
        GenerateDocumentation = symbolicDefinition.GenerateDocumentation
    };

    public NamedType? OriginalQuantity { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public NamedType? Scalar { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresVectorGroupDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSpecializedSharpMeasuresVectorGroupDefinition(SpecializedSharpMeasuresVectorGroupLocations locations) : base(locations) { }
}
