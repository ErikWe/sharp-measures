namespace SharpMeasures.Generators.Units.UnitInstances;

public interface IBiasedUnit : IDependantUnitInstance
{
    public abstract string From { get; }

    public abstract string Expression { get; }
}
