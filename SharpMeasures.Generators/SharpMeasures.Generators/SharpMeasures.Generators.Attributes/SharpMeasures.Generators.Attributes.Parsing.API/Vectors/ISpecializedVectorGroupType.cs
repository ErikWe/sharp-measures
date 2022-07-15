namespace SharpMeasures.Generators.Vectors;

public interface ISpecializedVectorGroupType : IVectorGroupType
{
    new public abstract ISpecializedVectorGroup Definition { get; }
}
