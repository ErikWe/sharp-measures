namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;

public class UnitDerivationSignature : ReadOnlyEquatableList<IRawUnitType>
{
    public UnitDerivationSignature(IReadOnlyList<IRawUnitType> units) : base(units) { }
}
