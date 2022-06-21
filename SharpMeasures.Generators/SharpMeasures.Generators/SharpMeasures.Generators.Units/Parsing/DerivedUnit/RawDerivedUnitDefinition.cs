namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawDerivedUnitDefinition : ARawUnitDefinition<DerivedUnitParsingData, DerivedUnitLocations>
{
    public static RawDerivedUnitDefinition Empty { get; } = new();

    public string? SignatureID { get; init; }
    public ReadOnlyEquatableList<string?> Units { get; init; } = ReadOnlyEquatableList<string?>.Empty;

    private RawDerivedUnitDefinition() : base(DerivedUnitLocations.Empty, DerivedUnitParsingData.Empty) { }
}
