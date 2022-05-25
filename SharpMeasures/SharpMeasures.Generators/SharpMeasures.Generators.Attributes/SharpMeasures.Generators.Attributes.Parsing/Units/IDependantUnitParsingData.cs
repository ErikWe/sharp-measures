namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IDependantUnitParsingData : IUnitParsingData
{
    new public abstract IDependantUnitLocations Locations { get; }
}
