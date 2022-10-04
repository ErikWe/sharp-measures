namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class RawSpecializedSharpMeasuresScalarDefinition : ARawAttributeDefinition<RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSpecializedSharpMeasuresScalarDefinition FromSymbolic(SymbolicSpecializedSharpMeasuresScalarDefinition symbolicDefinition) => new RawSpecializedSharpMeasuresScalarDefinition(symbolicDefinition.Locations) with
    {
        OriginalQuantity = symbolicDefinition.OriginalQuantity?.AsNamedType(),
        InheritDerivations = symbolicDefinition.InheritOperations,
        InheritProcesses = symbolicDefinition.InheritProcesses,
        InheritConstants = symbolicDefinition.InheritConstants,
        InheritConversions = symbolicDefinition.InheritConversions,
        InheritBases = symbolicDefinition.InheritBases,
        InheritUnits = symbolicDefinition.InheritUnits,
        Vector = symbolicDefinition.Vector?.AsNamedType(),
        ForwardsCastOperatorBehaviour = symbolicDefinition.ForwardsCastOperatorBehaviour,
        BackwardsCastOperatorBehaviour = symbolicDefinition.BackwardsCastOperatorBehaviour,
        ImplementSum = symbolicDefinition.ImplementSum,
        ImplementDifference = symbolicDefinition.ImplementDifference,
        Difference = symbolicDefinition.Difference?.AsNamedType(),
        DefaultUnitInstanceName = symbolicDefinition.DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol = symbolicDefinition.DefaultUnitInstanceSymbol,
        GenerateDocumentation = symbolicDefinition.GenerateDocumentation
    };

    public NamedType? OriginalQuantity { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritProcesses { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritBases { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Implicit;

    public NamedType? Vector { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresScalarDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSpecializedSharpMeasuresScalarDefinition(SpecializedSharpMeasuresScalarLocations locations) : base(locations) { }
}
