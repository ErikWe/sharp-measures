namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class DerivableUnit : AAttributeDefinition<DerivableUnitLocations>
{
    public string Expression { get; }
    public DerivableSignature Signature { get; }

    public DerivableUnit(string expression, DerivableSignature signature, DerivableUnitLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature;
    }
}
