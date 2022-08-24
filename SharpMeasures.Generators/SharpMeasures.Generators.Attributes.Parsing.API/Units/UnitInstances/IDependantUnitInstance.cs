namespace SharpMeasures.Generators.Units.UnitInstances;

public interface IDependantUnitInstance : IUnitInstance
{
    public abstract string DependantOn { get; }
}
