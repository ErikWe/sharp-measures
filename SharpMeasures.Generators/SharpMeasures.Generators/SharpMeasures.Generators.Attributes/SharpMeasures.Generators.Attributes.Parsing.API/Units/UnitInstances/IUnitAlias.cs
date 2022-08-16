namespace SharpMeasures.Generators.Units.UnitInstances;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IUnitAlias : IDependantUnitInstance
{
    public abstract IRawUnitInstance AliasOf { get; }
}
