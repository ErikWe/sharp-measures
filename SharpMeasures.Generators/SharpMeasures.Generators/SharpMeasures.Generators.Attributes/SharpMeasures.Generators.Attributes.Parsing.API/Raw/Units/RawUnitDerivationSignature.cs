namespace SharpMeasures.Generators.Raw.Units;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class RawUnitDerivationSignature : ReadOnlyEquatableList<NamedType>
{
    public RawUnitDerivationSignature(IReadOnlyList<NamedType> units) : base(units) { }
}
