namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public class QuantityDerivationSignature : ReadOnlyEquatableList<IRawQuantityType>
{
    public QuantityDerivationSignature(IReadOnlyList<IRawQuantityType> types) : base(types) { }
}
