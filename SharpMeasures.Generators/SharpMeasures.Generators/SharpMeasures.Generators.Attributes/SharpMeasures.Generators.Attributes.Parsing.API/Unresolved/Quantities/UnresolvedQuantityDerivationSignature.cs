namespace SharpMeasures.Generators.Unresolved.Quantities;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class UnresolvedQuantityDerivationSignature : ReadOnlyEquatableList<NamedType>
{
    public UnresolvedQuantityDerivationSignature(IReadOnlyList<NamedType> types) : base(types) { }
}
