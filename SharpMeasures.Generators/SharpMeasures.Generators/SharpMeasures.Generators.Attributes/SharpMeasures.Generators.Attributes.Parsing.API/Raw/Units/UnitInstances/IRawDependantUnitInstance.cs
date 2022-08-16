namespace SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IRawDependantUnitInstance : IRawUnitInstance
{
    public abstract string DependantOn { get; }
}
