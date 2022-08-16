namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IScaledUnit : IDependantUnitInstance
{
    public abstract IRawUnitInstance From { get; }

    public abstract string Expression { get; }
}
