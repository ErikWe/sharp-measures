namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

public record class QuantityConstantProcessingContext : SimpleProcessingContext, IQuantityConstantProcessingContext
{
    public HashSet<string> ReservedConstants { get; } = new();
    public HashSet<string> ReservedConstantMultiples { get; } = new();

    public QuantityConstantProcessingContext(DefinedType type) : base(type) { }
}
