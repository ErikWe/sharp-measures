namespace SharpMeasures.Attributes.Parsing.Units;

public interface IDerivedUnitInstanceAttributeParameters : IUnitInstanceAttributeParameters
{
    public abstract string DerivedFrom { get; }
}