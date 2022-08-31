namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal record class RawDerivableUnitDefinition : IOpenAttributeDefinition<RawDerivableUnitDefinition, DerivableUnitLocations>
{
    public static RawDerivableUnitDefinition Empty { get; } = new(DerivableUnitLocations.Empty);

    public string? Expression { get; init; }
    public string? DerivationID { get; init; }
    public IReadOnlyList<NamedType?>? Signature
    {
        get => signature;
        init => signature = value?.AsReadOnlyEquatable();
    }

    public bool Permutations { get; init; }

    private ReadOnlyEquatableList<NamedType?>? signature { get; init; }

    public DerivableUnitLocations Locations { get; private init; }

    private RawDerivableUnitDefinition(DerivableUnitLocations locations)
    {
        Locations = locations;
    }

    protected RawDerivableUnitDefinition WithLocations(DerivableUnitLocations locations) => this with
    {
        Locations = locations
    };

    RawDerivableUnitDefinition IOpenAttributeDefinition<RawDerivableUnitDefinition, DerivableUnitLocations>.WithLocations(DerivableUnitLocations locations)
        => WithLocations(locations);
}
