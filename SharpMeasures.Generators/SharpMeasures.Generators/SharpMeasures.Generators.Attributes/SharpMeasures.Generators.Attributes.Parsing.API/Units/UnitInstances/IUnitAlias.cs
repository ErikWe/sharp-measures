namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IUnitAlias : IDependantUnitInstance
{
    public abstract IUnresolvedUnitInstance AliasOf { get; }
}
