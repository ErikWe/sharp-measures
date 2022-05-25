namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IUnitParsingData : IAttributeParsingData
{
    public abstract string InterpretedPlural { get; }

    new public abstract IUnitLocations Locations { get; }
}
