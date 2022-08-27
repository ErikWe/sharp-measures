namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorConstant : IQuantityConstant
{
    public abstract IReadOnlyList<double> Value { get; }
}
