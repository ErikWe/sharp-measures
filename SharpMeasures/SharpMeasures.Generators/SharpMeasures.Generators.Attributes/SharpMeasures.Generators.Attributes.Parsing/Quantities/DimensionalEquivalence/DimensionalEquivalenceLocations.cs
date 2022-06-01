namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class DimensionalEquivalenceLocations : AItemListLocations
{
    internal static DimensionalEquivalenceLocations Empty { get; } = new();

    public MinimalLocation? QuantitiesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> QuantitiesElements => ItemsElements;

    public MinimalLocation? CastOperatorBehaviour { get; init; }

    public bool ExplicitlySetQuantities => QuantitiesCollection is not null;
    public bool ExplicitlySetCastOperatorBehaviour => CastOperatorBehaviour is not null;

    private DimensionalEquivalenceLocations() { }
}