namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedConvertibleScalar : IUnresolvedConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Scalars { get; }
}
