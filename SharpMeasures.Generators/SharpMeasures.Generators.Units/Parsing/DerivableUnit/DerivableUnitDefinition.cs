namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal record class DerivableUnitDefinition : IAttributeDefinition<DerivableUnitLocations>, IDerivableUnit
{
    public string? DerivationID { get; }

    public string Expression { get; }
    public IReadOnlyList<NamedType> Signature => signature;

    public bool Permutations { get; }

    private ReadOnlyEquatableList<NamedType> signature { get; }

    public DerivableUnitLocations Locations { get; }

    public DerivableUnitDefinition(string? derivationID, string expression, IReadOnlyList<NamedType> signature, bool permutations, DerivableUnitLocations locations)
    {
        DerivationID = derivationID;

        Expression = expression;
        this.signature = signature.AsReadOnlyEquatable();

        Permutations = permutations;

        Locations = locations;
    }
}
