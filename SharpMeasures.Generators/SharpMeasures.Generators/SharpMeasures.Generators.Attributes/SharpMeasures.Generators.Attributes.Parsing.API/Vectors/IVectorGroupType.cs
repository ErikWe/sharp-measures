namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

public interface IVectorGroupType : IQuantityType
{
    new public abstract IVectorGroup Definition { get; }

    public abstract IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> MembersByDimension { get; }

    new public abstract IReadOnlyList<IConvertibleVector> Conversions { get; }
}
