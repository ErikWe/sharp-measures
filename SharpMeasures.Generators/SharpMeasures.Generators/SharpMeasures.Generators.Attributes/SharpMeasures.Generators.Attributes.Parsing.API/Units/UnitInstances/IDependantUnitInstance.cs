namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IDependantUnitInstance : IUnitInstance
{
    public abstract IUnresolvedUnitInstance DependantOn { get; }
}
