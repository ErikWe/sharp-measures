namespace SharpMeasures.Generators.Unresolved.Units;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class UnresolvedUnitDerivationSignature : ReadOnlyEquatableList<NamedType>
{
    public UnresolvedUnitDerivationSignature(IReadOnlyList<NamedType> units) : base(units) { }
}
