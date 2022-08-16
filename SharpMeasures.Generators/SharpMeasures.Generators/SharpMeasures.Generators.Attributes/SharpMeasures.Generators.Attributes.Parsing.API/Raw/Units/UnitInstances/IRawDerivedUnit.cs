namespace SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

public interface IRawDerivedUnit : IRawUnitInstance
{
    public abstract string? DerivationID { get; }
    public abstract IReadOnlyList<string> Units { get; }
}
