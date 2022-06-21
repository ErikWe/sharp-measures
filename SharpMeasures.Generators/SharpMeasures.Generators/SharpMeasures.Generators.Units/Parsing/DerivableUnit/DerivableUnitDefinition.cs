namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class DerivableUnitDefinition : AAttributeDefinition<DerivableUnitLocations>
{
    public string DerivationID { get; }
    public string Expression { get; }
    public DerivableSignature Signature { get; }

    public DerivableUnitDefinition(string derivationID, string expression, DerivableSignature signature, DerivableUnitLocations locations) : base(locations)
    {
        Expression = expression;
        DerivationID = derivationID;
        Signature = signature;
    }
}
