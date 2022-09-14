namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawConvertibleQuantityDefinition : ARawItemListDefinition<NamedType?, RawConvertibleQuantityDefinition, ConvertibleQuantityLocations>
{
    public static RawConvertibleQuantityDefinition FromSymbolic(SymbolicConvertibleQuantityDefinition symbolicDefinition) => new RawConvertibleQuantityDefinition(symbolicDefinition.Locations).WithItems(symbolicDefinition.Quantities.AsNamedTypes()) with
    {
        ConversionDirection = symbolicDefinition.ConversionDirection,
        CastOperatorBehaviour = symbolicDefinition.CastOperatorBehaviour
    };

    public IReadOnlyList<NamedType?> Quantities => Items;

    public QuantityConversionDirection ConversionDirection { get; init; } = QuantityConversionDirection.Onedirectional;
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;

    protected override RawConvertibleQuantityDefinition Definition => this;

    protected RawConvertibleQuantityDefinition(ConvertibleQuantityLocations locations) : base(locations) { }
}
