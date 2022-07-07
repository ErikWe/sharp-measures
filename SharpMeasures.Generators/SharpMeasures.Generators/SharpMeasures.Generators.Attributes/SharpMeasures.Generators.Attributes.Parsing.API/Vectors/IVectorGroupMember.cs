namespace SharpMeasures.Generators.Vectors;

public interface IVectorGroupMember : IVector
{
    public abstract NamedType Group { get; }
}
