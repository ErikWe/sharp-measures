namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

public record class VectorConstantLocations : AAttributeLocations
{
    internal static VectorConstantLocations Empty { get; } = new();

    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? ValueCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ValueElements { get; init; } = Array.Empty<MinimalLocation>();

    public MinimalLocation? GenerateMultiplesProperty { get; init; }
    public MinimalLocation? MultiplesName { get; init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetValue => ValueCollection is not null;

    public bool ExplicitlySetGenerateMultiplesProperty => GenerateMultiplesProperty is not null;
    public bool ExplicitlySetMultiplesName => MultiplesName is not null;

    private VectorConstantLocations() { }

    public virtual bool Equals(VectorConstantLocations other)
    {
        if (other is null)
        {
            return false;
        }

        return Name == other.Name && Unit == other.Unit && ValueCollection == other.ValueCollection && GenerateMultiplesProperty == other.GenerateMultiplesProperty
            && MultiplesName == other.MultiplesName && ValueElements.SequenceEqual(other.ValueElements);
    }

    public override int GetHashCode()
    {
        return (Name, Unit, ValueCollection, GenerateMultiplesProperty, MultiplesName).GetHashCode() ^ ValueElements.GetSequenceHashCode();
    }
}
