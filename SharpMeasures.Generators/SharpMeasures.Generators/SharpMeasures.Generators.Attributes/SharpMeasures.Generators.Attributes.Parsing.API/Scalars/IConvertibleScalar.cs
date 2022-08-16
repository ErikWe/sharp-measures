namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Scalars;

using System.Collections.Generic;

public interface IConvertibleScalar : IConvertibleQuantity
{
    public abstract IReadOnlyList<IRawScalarType> Scalars { get; }
}
