namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class UnprocessedConvertibleQuantityDefinition : ARawItemListDefinition<NamedType?, UnprocessedConvertibleQuantityDefinition, ConvertibleQuantityLocations>
{
    internal static UnprocessedConvertibleQuantityDefinition Empty => new();

    public IReadOnlyList<NamedType?> Quantities => Items;

    public bool Bidirectional { get; init; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; }

    protected override UnprocessedConvertibleQuantityDefinition Definition => this;

    private UnprocessedConvertibleQuantityDefinition() : base(ConvertibleQuantityLocations.Empty) { }
}
