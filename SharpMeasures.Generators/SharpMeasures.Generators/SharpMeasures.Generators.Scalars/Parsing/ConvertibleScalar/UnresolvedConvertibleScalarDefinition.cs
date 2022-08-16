namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal record class UnresolvedConvertibleScalarDefinition : AItemListDefinition<NamedType, ConvertibleQuantityLocations>, IRawConvertibleScalar
{
    public IReadOnlyList<NamedType> Scalars => Items;

    IReadOnlyList<NamedType> IRawConvertibleQuantity.Quantities => Scalars;

    public bool Bidirectional { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    public UnresolvedConvertibleScalarDefinition(IReadOnlyList<NamedType> scalars, bool bidirectional, ConversionOperatorBehaviour castOperatorBehaviour, ConvertibleQuantityLocations locations, IReadOnlyList<int> locationMap)
        : base(scalars, locations, locationMap)
    {
        Bidirectional = bidirectional;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
