namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Generators.Attributes.Parsing;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class VectorConstantLocations : AAttributeLocations
{
    public static VectorConstantLocations Empty { get; } = new();

    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? ValueCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ValueElements { get; init; } = Array.Empty<MinimalLocation>();

    public MinimalLocation? GenerateMultiplesProperty { get; init; }
    public MinimalLocation? Multiples { get; init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetValue => ValueCollection is not null;

    public bool ExplicitlySetGenerateMultiplesProperty => GenerateMultiplesProperty is not null;
    public bool ExplicitlySetMultiples => Multiples is not null;

    private VectorConstantLocations() { }

    public virtual bool Equals(VectorConstantLocations other) => other is not null && Name == other.Name && Unit == other.Unit
        && ValueCollection == other.ValueCollection && GenerateMultiplesProperty == other.GenerateMultiplesProperty && Multiples == other.Multiples
        && ValueElements.SequenceEqual(other.ValueElements);

    public override int GetHashCode()
    {
        return (Name, Unit, ValueCollection, GenerateMultiplesProperty, Multiples).GetHashCode() ^ ValueElements.GetSequenceHashCode();
    }
}
