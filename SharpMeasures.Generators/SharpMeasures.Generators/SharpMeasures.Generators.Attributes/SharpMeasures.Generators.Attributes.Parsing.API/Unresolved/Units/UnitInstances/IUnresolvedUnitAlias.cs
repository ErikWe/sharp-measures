namespace SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IUnresolvedUnitAlias : IUnresolvedDependantUnitInstance
{
    public abstract string AliasOf { get; }
}
