namespace SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

using SharpMeasures.Generators.Attributes.Parsing;

public interface IDefaultUnitLocations : IAttributeLocations
{
    public abstract MinimalLocation? DefaultUnitName { get; }
    public abstract MinimalLocation? DefaultUnitSymbol { get; }

    public abstract bool ExplicitlySetDefaultUnitName { get; }
    public abstract bool ExplicitlySetDefaultUnitSymbol { get; }
}
