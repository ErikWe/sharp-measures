namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

public interface IUnresolvedDerivedUnit : IUnresolvedUnitInstance
{
    public abstract string? SignatureID { get; }
    public abstract IReadOnlyList<string> Units { get; }
}
