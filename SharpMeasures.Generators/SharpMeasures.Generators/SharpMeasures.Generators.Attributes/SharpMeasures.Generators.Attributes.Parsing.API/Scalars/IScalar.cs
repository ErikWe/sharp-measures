namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

public interface IScalar : IQuantity
{
    public abstract IReadOnlyList<IUnresolvedVectorType> Vectors { get; }

    new public abstract IUnresolvedScalarType? Difference { get; }

    public abstract IUnresolvedScalarType? Reciprocal { get; }
    public abstract IUnresolvedScalarType? Square { get; }
    public abstract IUnresolvedScalarType? Cube { get; }
    public abstract IUnresolvedScalarType? SquareRoot { get; }
    public abstract IUnresolvedScalarType? CubeRoot { get; }
}
