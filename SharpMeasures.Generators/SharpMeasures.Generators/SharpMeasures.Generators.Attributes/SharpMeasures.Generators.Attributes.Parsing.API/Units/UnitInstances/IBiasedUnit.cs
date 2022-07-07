namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IBiasedUnit : IDependantUnitInstance
{
    public abstract IUnresolvedUnitInstance From { get; }

    public abstract string Expression { get; }
}
