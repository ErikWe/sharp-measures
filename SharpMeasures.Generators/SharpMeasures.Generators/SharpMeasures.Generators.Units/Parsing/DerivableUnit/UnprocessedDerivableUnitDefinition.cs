namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal record class UnprocessedDerivableUnitDefinition : IOpenAttributeDefinition<UnprocessedDerivableUnitDefinition, DerivableUnitLocations>
{
    public static UnprocessedDerivableUnitDefinition Empty { get; } = new(DerivableUnitLocations.Empty);

    public string? Expression { get; init; }
    public string? DerivationID { get; init; }
    public IReadOnlyList<NamedType?>? Signature
    {
        get => signature;
        init => signature = value?.AsReadOnlyEquatable();
    }

    private ReadOnlyEquatableList<NamedType?>? signature { get; init; }

    public DerivableUnitLocations Locations { get; private init; }

    private UnprocessedDerivableUnitDefinition(DerivableUnitLocations locations)
    {
        Locations = locations;
    }

    protected UnprocessedDerivableUnitDefinition WithLocations(DerivableUnitLocations locations) => this with
    {
        Locations = locations
    };

    UnprocessedDerivableUnitDefinition IOpenAttributeDefinition<UnprocessedDerivableUnitDefinition, DerivableUnitLocations>.WithLocations(DerivableUnitLocations locations)
        => WithLocations(locations);
}
