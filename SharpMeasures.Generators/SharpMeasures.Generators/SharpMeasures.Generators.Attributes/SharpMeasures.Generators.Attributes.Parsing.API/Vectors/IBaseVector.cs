namespace SharpMeasures.Generators.Vectors;

public interface IBaseVector : IVector
{
    public abstract NamedType Unit { get; }

    new public abstract NamedType Difference { get; }
}
