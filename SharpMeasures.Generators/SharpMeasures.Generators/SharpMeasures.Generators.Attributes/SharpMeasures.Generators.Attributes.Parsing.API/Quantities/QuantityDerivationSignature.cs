namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class QuantityDerivationSignature : ReadOnlyEquatableList<NamedType>
{
    public QuantityDerivationSignature(IReadOnlyList<NamedType> types) : base(types) { }
}
