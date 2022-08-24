namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Collections.Generic;

public record class ConvertibleQuantityProcessingContext : SimpleProcessingContext, IConvertibleQuantityProcessingContext
{
    public HashSet<NamedType> ListedQuantities { get; } = new();

    public ConvertibleQuantityProcessingContext(DefinedType type) : base(type) { }
}
