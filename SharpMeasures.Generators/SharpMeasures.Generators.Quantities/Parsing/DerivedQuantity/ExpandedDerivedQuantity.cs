namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public record class ExpandedDerivedQuantity : IExpandedDerivedQuantity
{
    public string Expression { get; }
    public IReadOnlyList<NamedType> Signature => signature;

    private ReadOnlyEquatableList<NamedType> signature { get; }

    public ExpandedDerivedQuantity(string expression, IReadOnlyList<NamedType> signature)
    {
        Expression = expression;
        this.signature = signature.AsReadOnlyEquatable();
    }
}
