namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public record class DerivedQuantityProcessingContext : SimpleProcessingContext, IDerivedQuantityProcessingContext
{
    public HashSet<UnresolvedQuantityDerivationSignature> ListedDerivations { get; } = new();

    public DerivedQuantityProcessingContext(DefinedType type) : base(type) { }
}
