namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;

public interface IConvertibleScalar : IConvertibleQuantity
{
    public abstract IReadOnlyList<IUnresolvedScalarType> Scalars { get; }
}
