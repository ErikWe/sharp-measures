namespace SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public record class DimensionalEquivalenceLocations : AItemListLocations
{
    internal static DimensionalEquivalenceLocations Empty { get; } = new();

    public MinimalLocation? QuantitiesCollection => ItemsCollection;
    public ReadOnlyEquatableList<MinimalLocation> QuantitiesElements => ItemsElements;

    public MinimalLocation? CastOperatorBehaviour { get; init; }

    public bool ExplicitlySetQuantities => QuantitiesCollection is not null;
    public bool ExplicitlySetCastOperatorBehaviour => CastOperatorBehaviour is not null;

    private DimensionalEquivalenceLocations() { }
}
