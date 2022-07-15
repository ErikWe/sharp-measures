namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface ISpecializedVectorType : IVectorType, ISpecializedQuantityType
{
    new public abstract ISpecializedVector Definition { get; }
}
