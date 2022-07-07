namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using SharpMeasures.Generators.Units.UnitInstances;

public interface IUnresolvedUnitInstance
{
    public abstract string Name { get; }
    public abstract string Plural { get; }
}
