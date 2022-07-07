namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IUnresolvedBiasedUnit : IUnresolvedDependantUnitInstance
{
    public abstract string From { get; }

    public abstract string Expression { get; }
}
