namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class ConvertibleVectorDefinition : AItemListDefinition<IUnresolvedVectorGroupType, ConvertibleQuantityLocations>, IConvertibleVector
{
    public IReadOnlyList<IUnresolvedVectorGroupType> VectorGroups => Items;

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IReadOnlyList<IUnresolvedQuantityType> IConvertibleQuantity.Quantities => VectorGroups;

    public bool Bidirectional { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    public ConvertibleVectorDefinition(IReadOnlyList<IUnresolvedIndividualVectorType> vectors, bool bidirectional, ConversionOperatorBehaviour castOperatorBehaviour,
        ConvertibleQuantityLocations locations)
        : base(vectors, locations)
    {
        Bidirectional = bidirectional;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
