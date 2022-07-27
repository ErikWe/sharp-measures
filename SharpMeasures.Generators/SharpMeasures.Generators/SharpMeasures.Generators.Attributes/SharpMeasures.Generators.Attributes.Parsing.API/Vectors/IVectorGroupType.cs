namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorGroupType : IQuantityType
{
    new public abstract IVectorGroup Definition { get; }

    public abstract IReadOnlyDictionary<int, IRegisteredVectorGroupMember> RegisteredMembersByDimension { get; }

    new public abstract IReadOnlyList<IConvertibleVector> Conversions { get; }
}
