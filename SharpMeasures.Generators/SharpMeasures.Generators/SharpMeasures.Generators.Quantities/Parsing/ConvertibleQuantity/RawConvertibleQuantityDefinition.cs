namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class RawConvertibleQuantityDefinition : ARawItemListDefinition<NamedType?, RawConvertibleQuantityDefinition, ConvertibleQuantityLocations>
{
    internal static RawConvertibleQuantityDefinition Empty => new();

    public IReadOnlyList<NamedType?> Quantities => Items;

    public bool Bidirectional { get; init; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; }

    protected override RawConvertibleQuantityDefinition Definition => this;

    private RawConvertibleQuantityDefinition() : base(ConvertibleQuantityLocations.Empty) { }
}
