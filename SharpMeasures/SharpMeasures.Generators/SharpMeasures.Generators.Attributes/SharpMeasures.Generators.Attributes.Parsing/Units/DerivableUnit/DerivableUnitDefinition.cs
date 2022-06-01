namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class DerivableUnitDefinition : AAttributeDefinition<DerivableUnitLocations>
{
    public string Expression { get; }
    public DerivableSignature Signature { get; }

    public DerivableUnitDefinition(string expression, DerivableSignature signature, DerivableUnitLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature;
    }
}
