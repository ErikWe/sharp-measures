namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedBaseVectorType : IUnresolvedVectorType
{
    new public abstract IUnresolvedBaseVector Definition { get; }
}
