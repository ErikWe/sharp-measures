namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorConstant : IQuantityConstant
{
    public abstract IReadOnlyCollection<double> Value { get; }
}
