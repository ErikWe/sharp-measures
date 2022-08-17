namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IDefaultUnitDefinition
{
    public abstract IRawUnitInstance? DefaultUnit { get; }
    public abstract string? DefaultUnitSymbol { get; }

    public abstract IDefaultUnitLocations DefaultUnitLocations { get; }
}
