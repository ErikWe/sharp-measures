namespace SharpMeasures.Generators.Vectors;

public interface ISpecializedVectorGroupType : IVectorType
{
    new public abstract ISpecializedVectorGroup VectorDefinition { get; }
}
