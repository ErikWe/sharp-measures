namespace SharpMeasures.Generators.Units.UnitInstances;

public interface IScaledUnit : IDependantUnitInstance
{
    public abstract string From { get; }

    public abstract string Expression { get; }
}
