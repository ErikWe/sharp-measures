namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawVectorGroupMemberType : IRawQuantityType
{
    new public abstract IRawVectorGroupMember Definition { get; }

    public abstract IReadOnlyList<IRawVectorConstant> Constants { get; }
}
