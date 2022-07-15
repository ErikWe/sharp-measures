namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedSpecializedVectorType : IUnresolvedVectorType, IUnresolvedSpecializedQuantityType
{
    new public abstract IUnresolvedSpecializedVector Definition { get; }
}
