namespace SharpMeasures.Generators.Vectors;

public interface IBaseVectorGroup : IVectorGroup
{
    public abstract NamedType Unit { get; }

    new public abstract NamedType Difference { get; }
}
