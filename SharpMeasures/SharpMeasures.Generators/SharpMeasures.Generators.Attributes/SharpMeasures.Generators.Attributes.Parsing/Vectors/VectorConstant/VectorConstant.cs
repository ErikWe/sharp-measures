namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System.Collections.Generic;
using System.Linq;

public record class VectorConstant : AAttributeDefinition<VectorConstantLocations>
{
    public string Name { get; }
    public string Unit { get; }
    public IReadOnlyList<double> Value { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? MultiplesName { get; }

    public VectorConstant(string name, string unit, IReadOnlyList<double> value, bool generateMultiplesProperty, string? multiplesName, VectorConstantLocations locations)
        : base(locations)
    {
        Name = name;
        Unit = unit;
        Value = value;

        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;
    }

    public virtual bool Equals(VectorConstant other)
    {
        if (other is null)
        {
            return false;
        }

        return Name == other.Name && Unit == other.Unit && GenerateMultiplesProperty == other.GenerateMultiplesProperty && MultiplesName == other.MultiplesName
            && Value.SequenceEqual(other.Value);
    }

    public override int GetHashCode()
    {
        return (Name, Unit, GenerateMultiplesProperty, MultiplesName).GetHashCode() ^ Value.GetSequenceHashCode();
    }
}
