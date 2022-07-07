namespace SharpMeasures.Generators.Vectors;

public interface IBaseVectorGroupType : IVectorType
{
    new public abstract IBaseVectorGroup VectorDefinition { get; }
}
