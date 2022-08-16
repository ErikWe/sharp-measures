namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Quantities;

public record class UnresolvedDerivedQuantityDefinition : AAttributeDefinition<DerivedQuantityLocations>, IRawDerivedQuantity
{
    public string Expression { get; }
    public RawQuantityDerivationSignature Signature { get; }

    public bool ImplementOperators { get; }
    public bool ImplementAlgebraicallyEquivalentDerivations { get; }

    public UnresolvedDerivedQuantityDefinition(string expression, RawQuantityDerivationSignature signature, bool implementOperators,
        bool implementAlgebraicallyEquivalentDerivations, DerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature;

        ImplementOperators = implementOperators;
        ImplementAlgebraicallyEquivalentDerivations = implementAlgebraicallyEquivalentDerivations;
    }
}
