namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;

public record class DerivedQuantityDefinition : AAttributeDefinition<DerivedQuantityLocations>, IDerivedQuantity
{
    public string Expression { get; }
    public QuantityDerivationSignature Signature { get; }

    public bool ImplementOperators { get; }
    public bool ImplementAlgebraicallyEquivalentDerivations { get; }

    public DerivedQuantityDefinition(string expression, QuantityDerivationSignature signature, bool implementOperators, bool implementAlgebraicallyEquivalentDerivations,
        DerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature;

        ImplementOperators = implementOperators;
        ImplementAlgebraicallyEquivalentDerivations = implementAlgebraicallyEquivalentDerivations;
    }
}
