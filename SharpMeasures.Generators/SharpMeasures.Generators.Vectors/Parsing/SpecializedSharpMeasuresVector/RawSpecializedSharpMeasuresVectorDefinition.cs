namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class RawSpecializedSharpMeasuresVectorDefinition : ARawAttributeDefinition<RawSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSpecializedSharpMeasuresVectorDefinition FromSymbolic(SymbolicSpecializedSharpMeasuresVectorDefinition symbolicDefinition) => new RawSpecializedSharpMeasuresVectorDefinition(symbolicDefinition.Locations) with
    {
        OriginalQuantity = symbolicDefinition.OriginalQuantity?.AsNamedType(),
        InheritDerivations = symbolicDefinition.InheritDerivations,
        InheritConstants = symbolicDefinition.InheritConstants,
        InheritConversions = symbolicDefinition.InheritConversions,
        InheritUnits = symbolicDefinition.InheritUnits,
        ForwardsCastOperatorBehaviour = symbolicDefinition.ForwardsCastOperatorBehaviour,
        BackwardsCastOperatorBehaviour = symbolicDefinition.BackwardsCastOperatorBehaviour,
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

    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Implicit;

    public NamedType? Scalar { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresVectorDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSpecializedSharpMeasuresVectorDefinition(SpecializedSharpMeasuresVectorLocations locations) : base(locations) { }
}
