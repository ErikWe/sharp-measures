namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class ConvertibleQuantityDefinition : AItemListDefinition<NamedType, ConvertibleQuantityLocations>, IConvertibleQuantity
{
    public IReadOnlyList<NamedType> Quantities => Items;

    public bool Bidirectional { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    public ConvertibleQuantityDefinition(IReadOnlyList<NamedType> quantities, bool bidirectional, ConversionOperatorBehaviour castOperatorBehaviour,
        ConvertibleQuantityLocations locations)
        : base(quantities, locations)
    {
        Bidirectional = bidirectional;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
