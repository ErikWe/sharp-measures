namespace SharpMeasures.Generators.Vectors;

public interface IVectorGroupType : IVectorType
{
    new public abstract IVectorGroup VectorDefinition { get; }
}
