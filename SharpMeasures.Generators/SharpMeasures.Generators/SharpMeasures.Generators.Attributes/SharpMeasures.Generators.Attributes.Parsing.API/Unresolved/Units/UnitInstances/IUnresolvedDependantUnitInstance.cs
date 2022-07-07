namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using SharpMeasures.Generators.Units.UnitInstances;

public interface IUnresolvedDependantUnitInstance : IUnresolvedUnitInstance
{
    public abstract string DependantOn { get; }
}
