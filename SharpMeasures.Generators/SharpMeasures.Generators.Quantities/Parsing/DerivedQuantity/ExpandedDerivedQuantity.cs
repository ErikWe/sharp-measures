namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public sealed record class ExpandedDerivedQuantity : IExpandedDerivedQuantity
{
    public string Expression { get; }
    public IReadOnlyList<NamedType> Signature { get; }

    public ExpandedDerivedQuantity(string expression, IReadOnlyList<NamedType> signature)
    {
        Expression = expression;
        Signature = signature.AsReadOnlyEquatable();
    }
}
