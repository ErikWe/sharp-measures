namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public record class DerivedQuantityDefinition : AAttributeDefinition<DerivedQuantityLocations>, IDerivedQuantity
{
    public string Expression { get; }
    public IReadOnlyList<NamedType> Signature => signature;

    public bool ImplementOperators { get; }
    public bool ImplementAlgebraicallyEquivalentDerivations { get; }

    private ReadOnlyEquatableList<NamedType> signature { get; }

    public DerivedQuantityDefinition(string expression, IReadOnlyList<NamedType> signature, bool implementOperators,
        bool implementAlgebraicallyEquivalentDerivations, DerivedQuantityLocations locations) : base(locations)
    {
        Expression = expression;
        this.signature = signature.AsReadOnlyEquatable();

        ImplementOperators = implementOperators;
        ImplementAlgebraicallyEquivalentDerivations = implementAlgebraicallyEquivalentDerivations;
    }
}
