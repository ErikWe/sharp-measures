namespace SharpMeasures.Generators.Vectors;

public interface IVectorGroupMemberType : IIndividualVectorType
{
    new public abstract IVectorGroupMember Definition { get; }
}
