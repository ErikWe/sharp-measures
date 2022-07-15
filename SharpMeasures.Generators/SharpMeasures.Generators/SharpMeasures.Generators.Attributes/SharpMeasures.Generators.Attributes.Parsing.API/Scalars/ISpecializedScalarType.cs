namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface ISpecializedScalarType : IScalarType, ISpecializedQuantityType
{
    new public abstract ISpecializedScalar Definition { get; }
}
