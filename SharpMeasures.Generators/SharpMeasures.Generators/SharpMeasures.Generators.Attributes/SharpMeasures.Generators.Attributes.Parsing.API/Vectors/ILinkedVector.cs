namespace SharpMeasures.Generators.Vectors;

public interface ILinkedVector : IVector
{
    public abstract NamedType LinkedTo { get; }
}
