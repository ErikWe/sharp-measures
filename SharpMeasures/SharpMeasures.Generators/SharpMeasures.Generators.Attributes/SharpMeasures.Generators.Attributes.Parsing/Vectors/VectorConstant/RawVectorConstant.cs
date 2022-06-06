namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

public record class RawVectorConstant : ARawAttributeDefinition<VectorConstantLocations>
{
    internal static RawVectorConstant Empty { get; } = new();

    public string? Name { get; init; }
    public string? Unit { get; init; }
    public IReadOnlyList<double> Value { get; init; } = Array.Empty<double>();

    public bool GenerateMultiplesProperty { get; init; }
    public string? MultiplesName { get; init; }

    public VectorConstantParsingData ParsingData { get; init; } = VectorConstantParsingData.Empty;

    private RawVectorConstant() : base(VectorConstantLocations.Empty) { }

    public virtual bool Equals(RawVectorConstant other)
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
