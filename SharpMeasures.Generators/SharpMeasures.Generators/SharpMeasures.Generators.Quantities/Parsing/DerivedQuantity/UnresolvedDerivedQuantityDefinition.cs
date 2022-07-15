namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Quantities;

public record class UnresolvedDerivedQuantityDefinition : AAttributeDefinition<DerivedQuantityLocations>, IUnresolvedDerivedQuantity
{
    public string Expression { get; }
    public UnresolvedQuantityDerivationSignature Signature { get; }

    public bool ImplementOperators { get; }
    public bool ImplementAlgebraicallyEquivalentDerivations { get; }

    public UnresolvedDerivedQuantityDefinition(string expression, UnresolvedQuantityDerivationSignature signature, bool implementOperators,
        bool implementAlgebraicallyEquivalentDerivations, DerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        Signature = signature;

        ImplementOperators = implementOperators;
        ImplementAlgebraicallyEquivalentDerivations = implementAlgebraicallyEquivalentDerivations;
    }
}
