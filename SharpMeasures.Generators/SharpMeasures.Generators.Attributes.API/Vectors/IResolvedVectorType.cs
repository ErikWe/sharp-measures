namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedVectorType : IResolvedQuantityType
{
    public abstract int Dimension { get; }

    public abstract NamedType? Scalar { get; }

    public abstract IReadOnlyList<IVectorConstant> Constants { get; }
}
