namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SpecializedSharpMeasuresVectorGroupLocations : AAttributeLocations<SpecializedSharpMeasuresVectorGroupLocations>
{
    public static SpecializedSharpMeasuresVectorGroupLocations Empty => new();

    public MinimalLocation? OriginalVectorGroup { get; init; }

    public MinimalLocation? InheritDerivations { get; init; }
    public MinimalLocation? InheritConstants { get; init; }
    public MinimalLocation? InheritConversions { get; init; }
    public MinimalLocation? InheritUnits { get; init; }

    public MinimalLocation? Scalar { get; init; }

    public MinimalLocation? ImplementSum { get; init; }
    public MinimalLocation? ImplementDifference { get; init; }

    public MinimalLocation? Difference { get; init; }

    public MinimalLocation? DefaultUnitName { get; init; }
    public MinimalLocation? DefaultUnitSymbol { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetOriginalVectorGroup => OriginalVectorGroup is not null;
    public bool ExplicitlySetInheritDerivations => InheritDerivations is not null;
    public bool ExplicitlySetInheritConstants => InheritConstants is not null;
    public bool ExplicitlySetInheritConversions => InheritConversions is not null;
    public bool ExplicitlySetInheritUnits => InheritUnits is not null;
    public bool ExplicitlySetScalar => Scalar is not null;
    public bool ExplicitlySetImplementSum => ImplementSum is not null;
    public bool ExplicitlySetImplementDifference => ImplementDifference is not null;
    public bool ExplicitlySetDifference => Difference is not null;
    public bool ExplicitlySetDefaultUnitName => DefaultUnitName is not null;
    public bool ExplicitlySetDefaultUnitSymbol => DefaultUnitSymbol is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SpecializedSharpMeasuresVectorGroupLocations Locations => this;

    private SpecializedSharpMeasuresVectorGroupLocations() { }
}
