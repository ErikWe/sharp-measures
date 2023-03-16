namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class ConvertibleQuantityLocations : AItemListLocations<ConvertibleQuantityLocations>, IConvertibleQuantityLocations
{
    internal static ConvertibleQuantityLocations Empty { get; } = new();

    public MinimalLocation? QuantitiesCollection => ItemsCollection;
    public IReadOnlyList<MinimalLocation> QuantitiesElements => ItemsElements;

    public MinimalLocation? ConversionDirection { get; init; }
    public MinimalLocation? CastOperatorBehaviour { get; init; }

    public bool ExplicitlySetQuantities => QuantitiesCollection is not null;

    public bool ExplicitlySetConversionDirection => ConversionDirection is not null;
    public bool ExplicitlySetCastOperatorBehaviour => CastOperatorBehaviour is not null;

    protected override ConvertibleQuantityLocations Locations => this;

    private ConvertibleQuantityLocations() { }
}
