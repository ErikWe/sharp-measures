namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class DerivableUnitDefinition : AAttributeDefinition<DerivableUnitLocations>
{
    public string Expression { get; }
    public DerivableSignature Signature { get; }

    public DerivableUnitDefinition(string expression, DerivableSignature signature, DerivableUnitLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature;
    }
}
