namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawDerivableUnitDefinition : ARawAttributeDefinition<DerivableUnitLocations>
{
    public static RawDerivableUnitDefinition Empty { get; } = new();

    public string? Expression { get; init; }
    public string? DerivationID { get; init; }
    public ReadOnlyEquatableList<NamedType?> Signature { get; init; } = ReadOnlyEquatableList<NamedType?>.Empty;

    private RawDerivableUnitDefinition() : base(DerivableUnitLocations.Empty) { }
}
