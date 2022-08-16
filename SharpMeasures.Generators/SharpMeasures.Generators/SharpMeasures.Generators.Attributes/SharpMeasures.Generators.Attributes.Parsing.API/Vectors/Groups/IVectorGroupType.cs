namespace SharpMeasures.Generators.Vectors.Groups;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Vectors.Groups;

using System.Collections.Generic;

public interface IVectorGroupType : IQuantityType
{
    new public abstract IVectorGroup Definition { get; }

    public abstract IReadOnlyDictionary<int, IRawVectorGroupMemberType> MembersByDimension { get; }

    new public abstract IReadOnlyList<IConvertibleVector> Conversions { get; }
}
