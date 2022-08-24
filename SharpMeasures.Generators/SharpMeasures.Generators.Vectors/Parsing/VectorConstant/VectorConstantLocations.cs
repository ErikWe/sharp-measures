namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

internal record class VectorConstantLocations : AQuantityConstantLocations<VectorConstantLocations>
{
    public static VectorConstantLocations Empty { get; } = new();

    public MinimalLocation? ValueCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ValueElements
    {
        get => valueElements;
        init => valueElements = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetValue => ValueCollection is not null;

    private ReadOnlyEquatableList<MinimalLocation> valueElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected override VectorConstantLocations Locations => this;

    private VectorConstantLocations() { }
}
