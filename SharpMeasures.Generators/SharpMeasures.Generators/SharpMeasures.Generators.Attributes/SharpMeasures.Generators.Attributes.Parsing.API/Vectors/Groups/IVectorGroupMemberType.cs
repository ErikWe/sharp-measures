namespace SharpMeasures.Generators.Vectors.Groups;
public interface IVectorGroupMemberType : IVectorType
{
    new public abstract IVectorGroupMember Definition { get; }
}
