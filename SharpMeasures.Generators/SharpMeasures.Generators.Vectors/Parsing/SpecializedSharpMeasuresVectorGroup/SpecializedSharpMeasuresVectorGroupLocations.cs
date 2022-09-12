namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class SpecializedSharpMeasuresVectorGroupLocations : AAttributeLocations<SpecializedSharpMeasuresVectorGroupLocations>, IVectorGroupSpecializationLocations, IDefaultUnitInstanceLocations
{
    public static SpecializedSharpMeasuresVectorGroupLocations Empty => new();

    public MinimalLocation? OriginalQuantity { get; init; }

    public MinimalLocation? InheritDerivations { get; init; }
    public MinimalLocation? InheritConstants { get; init; }
    public MinimalLocation? InheritConversions { get; init; }
    public MinimalLocation? InheritUnits { get; init; }

    public MinimalLocation? Scalar { get; init; }

    public MinimalLocation? ImplementSum { get; init; }
    public MinimalLocation? ImplementDifference { get; init; }
    public MinimalLocation? Difference { get; init; }

    public MinimalLocation? DefaultUnitInstanceName { get; init; }
    public MinimalLocation? DefaultUnitInstanceSymbol { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetOriginalQuantity => OriginalQuantity is not null;

    public bool ExplicitlySetInheritDerivations => InheritDerivations is not null;
    public bool ExplicitlySetInheritConstants => InheritConstants is not null;
    public bool ExplicitlySetInheritConversions => InheritConversions is not null;
    public bool ExplicitlySetInheritUnits => InheritUnits is not null;

    public bool ExplicitlySetScalar => Scalar is not null;

    public bool ExplicitlySetImplementSum => ImplementSum is not null;
    public bool ExplicitlySetImplementDifference => ImplementDifference is not null;
    public bool ExplicitlySetDifference => Difference is not null;

    public bool ExplicitlySetDefaultUnitInstanceName => DefaultUnitInstanceName is not null;
    public bool ExplicitlySetDefaultUnitInstanceSymbol => DefaultUnitInstanceSymbol is not null;

    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SpecializedSharpMeasuresVectorGroupLocations Locations => this;

    private SpecializedSharpMeasuresVectorGroupLocations() { }
}
