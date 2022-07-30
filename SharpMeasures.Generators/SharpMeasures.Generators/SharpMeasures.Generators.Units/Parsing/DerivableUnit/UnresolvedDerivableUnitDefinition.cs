namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Units;

internal record class UnresolvedDerivableUnitDefinition : IAttributeDefinition<DerivableUnitLocations>, IUnresolvedDerivableUnit
{
    public string? DerivationID { get; }

    public string Expression { get; }
    public UnresolvedUnitDerivationSignature Signature { get; }

    public DerivableUnitLocations Locations { get; }

    public UnresolvedDerivableUnitDefinition(string? derivationID, string expression, UnresolvedUnitDerivationSignature signature, DerivableUnitLocations locations)
    {
        DerivationID = derivationID;

        Expression = expression;
        Signature = signature;

        Locations = locations;
    }
}
