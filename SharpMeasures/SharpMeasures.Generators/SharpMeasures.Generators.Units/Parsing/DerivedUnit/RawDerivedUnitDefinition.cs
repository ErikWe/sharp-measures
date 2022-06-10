namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawDerivedUnitDefinition : ARawUnitDefinition<DerivedUnitParsingData, DerivedUnitLocations>
{
    public static RawDerivedUnitDefinition Empty { get; } = new();

    public ReadOnlyEquatableList<NamedType?> Signature { get; init; } = ReadOnlyEquatableList<NamedType?>.Empty;
    public ReadOnlyEquatableList<string?> Units { get; init; } = ReadOnlyEquatableList<string?>.Empty;

    private RawDerivedUnitDefinition() : base(DerivedUnitLocations.Empty, DerivedUnitParsingData.Empty) { }
}
