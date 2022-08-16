namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawVectorConstant : IRawQuantityConstant
{
    public abstract IReadOnlyList<double> Value { get; }
}
