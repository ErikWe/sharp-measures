namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IUnresolvedScaledUnit : IUnresolvedDependantUnitInstance
{
    public abstract string From { get; }

    public abstract string Expression { get; }
}
