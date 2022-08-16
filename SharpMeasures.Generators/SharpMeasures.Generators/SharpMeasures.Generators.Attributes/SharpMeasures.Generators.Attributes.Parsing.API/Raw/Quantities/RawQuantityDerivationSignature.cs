namespace SharpMeasures.Generators.Raw.Quantities;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class RawQuantityDerivationSignature : ReadOnlyEquatableList<NamedType>
{
    public RawQuantityDerivationSignature(IReadOnlyList<NamedType> types) : base(types) { }
}
