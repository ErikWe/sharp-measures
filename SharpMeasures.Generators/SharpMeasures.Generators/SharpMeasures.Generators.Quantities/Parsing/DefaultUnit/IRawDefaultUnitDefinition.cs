namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

public interface IRawDefaultUnitDefinition
{
    public abstract string? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }

    public abstract IDefaultUnitLocations DefaultUnitLocations { get; }
}
