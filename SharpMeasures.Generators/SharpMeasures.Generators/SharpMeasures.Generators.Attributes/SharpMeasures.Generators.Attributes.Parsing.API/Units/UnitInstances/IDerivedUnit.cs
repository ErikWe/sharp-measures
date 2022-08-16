namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;

public interface IDerivedUnit : IUnitInstance
{
    public abstract RawUnitDerivationSignature Signature { get; }
    public abstract IReadOnlyList<IRawUnitInstance> Units { get; }
}
