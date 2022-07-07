namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;

public interface IDerivedUnit : IUnitInstance
{
    public abstract UnresolvedUnitDerivationSignature Signature { get; }
    public abstract IReadOnlyList<IUnresolvedUnitInstance> Units { get; }
}
