namespace SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IRawUnitAlias : IRawDependantUnitInstance
{
    public abstract string AliasOf { get; }
}
