namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public class QuantityDerivationSignature : ReadOnlyEquatableList<IUnresolvedQuantityType>
{
    public QuantityDerivationSignature(IReadOnlyList<IUnresolvedQuantityType> types) : base(types) { }
}
