namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawConvertibleQuantityDefinition : ARawItemListDefinition<NamedType?, RawConvertibleQuantityDefinition, ConvertibleQuantityLocations>
{
    internal static RawConvertibleQuantityDefinition Empty => new();

    public IReadOnlyList<NamedType?> Quantities => Items;

    public QuantityConversionDirection ConversionDirection { get; init; } = QuantityConversionDirection.Onedirectional;
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;

    protected override RawConvertibleQuantityDefinition Definition => this;

    protected RawConvertibleQuantityDefinition() : base(ConvertibleQuantityLocations.Empty) { }
}
