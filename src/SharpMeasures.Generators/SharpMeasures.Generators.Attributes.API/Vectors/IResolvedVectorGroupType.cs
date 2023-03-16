namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedVectorGroupType : IResolvedQuantityType
{
    public abstract NamedType? Scalar { get; }

    public abstract IReadOnlyDictionary<int, NamedType> MembersByDimension { get; }

    public abstract IReadOnlyList<IVectorOperation> VectorOperations { get; }

    public abstract IReadOnlyList<IVectorOperation> InheritedVectorOperations { get; }
}
