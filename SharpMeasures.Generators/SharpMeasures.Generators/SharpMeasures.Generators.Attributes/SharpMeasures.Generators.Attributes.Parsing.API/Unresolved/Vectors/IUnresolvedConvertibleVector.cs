namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedConvertibleVector : IUnresolvedConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> VectorGroups { get; }
}
