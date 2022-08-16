namespace SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IRawScaledUnit : IRawDependantUnitInstance
{
    public abstract string From { get; }

    public abstract string Expression { get; }
}
