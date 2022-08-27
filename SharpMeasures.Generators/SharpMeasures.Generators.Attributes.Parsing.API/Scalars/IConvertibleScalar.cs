namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IConvertibleScalar : IConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Scalars { get; }
}
