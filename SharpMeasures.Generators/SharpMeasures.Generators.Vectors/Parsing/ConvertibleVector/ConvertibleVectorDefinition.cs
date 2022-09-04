namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Collections.Generic;

public record class ConvertibleVectorDefinition : AItemListDefinition<NamedType, ConvertibleQuantityLocations>, IConvertibleQuantity
{
    public IReadOnlyList<NamedType> Quantities => Items;

    public QuantityConversionDirection ConversionDirection { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    IConvertibleQuantityLocations IConvertibleQuantity.Locations => Locations;

    public ConvertibleVectorDefinition(IReadOnlyList<NamedType> quantities, QuantityConversionDirection conversionDirection, ConversionOperatorBehaviour castOperatorBehaviour, ConvertibleQuantityLocations locations, IReadOnlyList<int> locationMap)
        : base(quantities, locations, locationMap)
    {
        ConversionDirection = conversionDirection;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
