namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IDependantUnitInstance : IUnitInstance
{
    public abstract IRawUnitInstance DependantOn { get; }
}
