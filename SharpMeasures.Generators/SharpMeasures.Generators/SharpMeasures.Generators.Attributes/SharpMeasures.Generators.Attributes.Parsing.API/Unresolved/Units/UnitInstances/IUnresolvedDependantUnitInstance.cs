namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IUnresolvedDependantUnitInstance : IUnresolvedUnitInstance
{
    public abstract string DependantOn { get; }
}
