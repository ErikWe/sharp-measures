namespace SharpMeasures.Generators.Vectors;

public interface IBaseVectorType : IVectorType
{
    new public abstract IBaseVector Definition { get; }
}
