namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawConvertibleVector : IRawConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Vectors { get; }
}
