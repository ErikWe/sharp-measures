namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public interface IDerivedUnitAttributeParameters : IUnitAttributeParameters
{
    public abstract string DerivedFrom { get; }
}