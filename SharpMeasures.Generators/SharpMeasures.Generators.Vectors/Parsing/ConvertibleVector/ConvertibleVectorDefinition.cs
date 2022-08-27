namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class ConvertibleVectorDefinition : AItemListDefinition<NamedType, ConvertibleQuantityLocations>, IConvertibleVector
{
    public IReadOnlyList<NamedType> Vectors => Items;

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IReadOnlyList<NamedType> IConvertibleQuantity.Quantities => Vectors;

    public bool Bidirectional { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    public ConvertibleVectorDefinition(IReadOnlyList<NamedType> vectors, bool bidirectional, ConversionOperatorBehaviour castOperatorBehaviour, ConvertibleQuantityLocations locations, IReadOnlyList<int> locationMap)
        : base(vectors, locations, locationMap)
    {
        Bidirectional = bidirectional;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
