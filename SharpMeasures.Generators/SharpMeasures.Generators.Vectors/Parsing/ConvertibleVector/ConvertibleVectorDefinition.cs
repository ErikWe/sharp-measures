namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class ConvertibleVectorDefinition : AItemListDefinition<NamedType, ConvertibleQuantityLocations>, IConvertibleQuantity
{
    public IReadOnlyList<NamedType> Quantities => Items;

    public bool Bidirectional { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    IConvertibleQuantityLocations IConvertibleQuantity.Locations => Locations;

    public ConvertibleVectorDefinition(IReadOnlyList<NamedType> quantities, bool bidirectional, ConversionOperatorBehaviour castOperatorBehaviour, ConvertibleQuantityLocations locations, IReadOnlyList<int> locationMap)
        : base(quantities, locations, locationMap)
    {
        Bidirectional = bidirectional;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
