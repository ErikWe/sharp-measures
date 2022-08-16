namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public record class DerivedQuantityProcessingContext : SimpleProcessingContext, IDerivedQuantityProcessingContext
{
    public HashSet<RawQuantityDerivationSignature> ListedDerivations { get; } = new();

    public DerivedQuantityProcessingContext(DefinedType type) : base(type) { }
}
