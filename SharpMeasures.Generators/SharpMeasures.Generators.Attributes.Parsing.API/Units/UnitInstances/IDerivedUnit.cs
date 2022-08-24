namespace SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

public interface IDerivedUnit : IUnitInstance
{
    public abstract string? DerivationID { get; }
    public abstract IReadOnlyList<string> Units { get; }
}
