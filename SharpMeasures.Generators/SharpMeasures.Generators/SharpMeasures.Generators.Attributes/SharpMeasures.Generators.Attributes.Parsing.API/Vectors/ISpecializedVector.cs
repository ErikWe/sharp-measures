namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;

public interface ISpecializedVector : IVector, ISpecializedQuantity
{
    public abstract IUnresolvedVectorType OriginalVector { get; }

    public abstract bool InheritConvertibleVectors { get; }
}
