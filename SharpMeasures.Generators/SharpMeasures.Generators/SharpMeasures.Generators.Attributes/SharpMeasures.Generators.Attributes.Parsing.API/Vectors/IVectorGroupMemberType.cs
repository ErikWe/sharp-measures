namespace SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

public interface IVectorGroupMemberType : IIndividualVectorType
{
    new public abstract IVectorGroupMember Definition { get; }
}
