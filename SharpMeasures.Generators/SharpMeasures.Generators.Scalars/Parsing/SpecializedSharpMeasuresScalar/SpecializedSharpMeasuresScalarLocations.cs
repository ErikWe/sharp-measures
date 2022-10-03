namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class SpecializedSharpMeasuresScalarLocations : AAttributeLocations<SpecializedSharpMeasuresScalarLocations>, IScalarSpecializationLocations, IDefaultUnitInstanceLocations
{
    public static SpecializedSharpMeasuresScalarLocations Empty => new();

    public MinimalLocation? OriginalQuantity { get; init; }

    public MinimalLocation? InheritOperations { get; init; }
    public MinimalLocation? InheritProcesses { get; init; }
    public MinimalLocation? InheritConstants { get; init; }
    public MinimalLocation? InheritConversions { get; init; }
    public MinimalLocation? InheritBases { get; init; }
    public MinimalLocation? InheritUnits { get; init; }

    public MinimalLocation? ForwardsCastOperatorBehaviour { get; init; }
    public MinimalLocation? BackwardsCastOperatorBehaviour { get; init; }

    public MinimalLocation? Vector { get; init; }

    public MinimalLocation? ImplementSum { get; init; }
    public MinimalLocation? ImplementDifference { get; init; }
    public MinimalLocation? Difference { get; init; }

    public MinimalLocation? DefaultUnitInstanceName { get; init; }
    public MinimalLocation? DefaultUnitInstanceSymbol { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetOriginalQuantity => OriginalQuantity is not null;

    public bool ExplicitlySetInheritOperations => InheritOperations is not null;
    public bool ExplicitlySetInheritProcesses => InheritProcesses is not null;
    public bool ExplicitlySetInheritConstants => InheritConstants is not null;
    public bool ExplicitlySetInheritConversions => InheritConversions is not null;
    public bool ExplicitlySetInheritBases => InheritBases is not null;
    public bool ExplicitlySetInheritUnits => InheritUnits is not null;

    public bool ExplicitlySetForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour is not null;
    public bool ExplicitlySetBackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour is not null;

    public bool ExplicitlySetVector => Vector is not null;

    public bool ExplicitlySetImplementSum => ImplementSum is not null;
    public bool ExplicitlySetImplementDifference => ImplementDifference is not null;
    public bool ExplicitlySetDifference => Difference is not null;

    public bool ExplicitlySetDefaultUnitInstanceName => DefaultUnitInstanceName is not null;
    public bool ExplicitlySetDefaultUnitInstanceSymbol => DefaultUnitInstanceSymbol is not null;

    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SpecializedSharpMeasuresScalarLocations Locations => this;

    private SpecializedSharpMeasuresScalarLocations() { }
}
