namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

public interface IDefaultUnitDefinition
{
    public abstract string? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }

    public abstract IDefaultUnitLocations DefaultUnitLocations { get; }
}
