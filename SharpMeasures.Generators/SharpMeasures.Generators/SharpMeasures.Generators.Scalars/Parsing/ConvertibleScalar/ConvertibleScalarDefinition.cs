namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class ConvertibleScalarDefinition : AItemListDefinition<IRawScalarType, ConvertibleQuantityLocations>, IConvertibleScalar
{
    public IReadOnlyList<IRawScalarType> Scalars => Items;

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IReadOnlyList<IRawQuantityType> IConvertibleQuantity.Quantities => Scalars;

    public bool Bidirectional { get; }
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    public ConvertibleScalarDefinition(IReadOnlyList<IRawScalarType> scalars, bool bidirectional, ConversionOperatorBehaviour castOperatorBehaviour, ConvertibleQuantityLocations locations)
        : base(scalars, locations)
    {
        Bidirectional = bidirectional;
        CastOperatorBehaviour = castOperatorBehaviour;
    }
}
