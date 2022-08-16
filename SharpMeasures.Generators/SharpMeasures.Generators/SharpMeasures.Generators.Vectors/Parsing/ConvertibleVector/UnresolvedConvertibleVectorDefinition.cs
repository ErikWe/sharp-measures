namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal record class UnresolvedConvertibleVectorDefinition : AItemListDefinition<NamedType, ConvertibleQuantityLocations>, IRawConvertibleVectorGroup
{
    public IReadOnlyList<NamedType> VectorGroups => Items;

    IReadOnlyList<NamedType> IRawConvertibleQuantity.Quantities => VectorGroups;

    public bool Bidirectional { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    public UnresolvedConvertibleVectorDefinition(IReadOnlyList<NamedType> vectorGroups, bool bidirectional, ConversionOperatorBehaviour castOperatorBehaviour, ConvertibleQuantityLocations locations, IReadOnlyList<int> locationMap)
        : base(vectorGroups, locations, locationMap)
    {
        Bidirectional = bidirectional;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
