namespace SharpMeasures.Generators.Vectors;

public interface ISpecializedVectorType : IVectorType
{
    new public abstract ISpecializedVector VectorDefinition { get; }
}
