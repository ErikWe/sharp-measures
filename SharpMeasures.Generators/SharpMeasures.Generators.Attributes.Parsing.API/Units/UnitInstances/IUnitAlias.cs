namespace SharpMeasures.Generators.Units.UnitInstances;

public interface IUnitAlias : IDependantUnitInstance
{
    public abstract string AliasOf { get; }
}
