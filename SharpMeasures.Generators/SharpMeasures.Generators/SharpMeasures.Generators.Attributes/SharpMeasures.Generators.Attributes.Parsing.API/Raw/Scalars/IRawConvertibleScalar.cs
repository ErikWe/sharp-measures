namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawConvertibleScalar : IRawConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Scalars { get; }
}
