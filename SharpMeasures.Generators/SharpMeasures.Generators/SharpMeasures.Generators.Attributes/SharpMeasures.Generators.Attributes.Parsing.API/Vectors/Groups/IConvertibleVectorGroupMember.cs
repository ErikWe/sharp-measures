namespace SharpMeasures.Generators.Vectors;

using OneOf;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;

using System.Collections.Generic;

public interface IConvertibleVectorGroupMember : IConvertibleQuantity
{
    public abstract IReadOnlyList<OneOf<IRawVectorType, IRawVectorGroupType, IRawVectorGroupMemberType>> Vectors { get; }
}
