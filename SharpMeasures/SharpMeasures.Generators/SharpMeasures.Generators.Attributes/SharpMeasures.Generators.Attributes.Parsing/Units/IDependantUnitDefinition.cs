namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IDependantUnitDefinition : IUnitDefinition
{
    public abstract string DependantOn { get; }

    new public abstract IDependantUnitParsingData ParsingData { get; }
}