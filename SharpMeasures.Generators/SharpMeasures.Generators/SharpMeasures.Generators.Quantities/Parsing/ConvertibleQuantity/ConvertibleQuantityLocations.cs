namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ConvertibleQuantityLocations : AItemListLocations<ConvertibleQuantityLocations>
{
    internal static ConvertibleQuantityLocations Empty { get; } = new();

    public MinimalLocation? QuantitiesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> QuantitiesElements => ItemsElements;

    public MinimalLocation? Bidirectional { get; init; }
    public MinimalLocation? CastOperatorBehaviour { get; init; }

    public bool ExplicitlySetQuantities => QuantitiesCollection is not null;
    public bool ExplicitlySetCastOperatorBehaviour => CastOperatorBehaviour is not null;

    protected override ConvertibleQuantityLocations Locations => this;

    private ConvertibleQuantityLocations() { }
}
