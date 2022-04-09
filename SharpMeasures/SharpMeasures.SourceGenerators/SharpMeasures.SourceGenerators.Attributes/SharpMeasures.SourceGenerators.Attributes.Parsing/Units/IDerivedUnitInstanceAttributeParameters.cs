namespace SharpMeasures.SourceGeneration.Attributes.Parsing.Units;

public interface IDerivedUnitInstanceAttributeParameters : IUnitInstanceAttributeParameters
{
    public abstract string DerivedFrom { get; }
}