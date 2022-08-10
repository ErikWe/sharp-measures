namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal interface IIntermediateVectorGroupMemberType
{
    public DefinedType Type { get; }

    public abstract IVectorGroupMember Definition { get; }

    public abstract IReadOnlyList<IVectorConstant> Constants { get; }
}
