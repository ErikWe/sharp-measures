namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IDependantUnitDefinitionParameters : IUnitDefinitionParameters
{
    public abstract string DependantOn { get; }
}