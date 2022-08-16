namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Units;

internal record class UnresolvedDerivableUnitDefinition : IAttributeDefinition<DerivableUnitLocations>, IRawDerivableUnit
{
    public string? DerivationID { get; }

    public string Expression { get; }
    public RawUnitDerivationSignature Signature { get; }

    public DerivableUnitLocations Locations { get; }

    public UnresolvedDerivableUnitDefinition(string? derivationID, string expression, RawUnitDerivationSignature signature, DerivableUnitLocations locations)
    {
        DerivationID = derivationID;

        Expression = expression;
        Signature = signature;

        Locations = locations;
    }
}
