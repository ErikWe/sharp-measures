namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedVectorGroupMember : IUnresolvedVector
{
    public abstract NamedType Group { get; }
}
