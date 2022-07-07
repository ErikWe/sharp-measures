namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;

public class UnitDerivationSignature : ReadOnlyEquatableList<IUnresolvedUnitType>
{
    public UnitDerivationSignature(IReadOnlyList<IUnresolvedUnitType> units) : base(units) { }
}
