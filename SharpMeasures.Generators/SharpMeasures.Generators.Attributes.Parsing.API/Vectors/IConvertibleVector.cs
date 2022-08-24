namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IConvertibleVector : IConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Vectors { get; }
}
