namespace SharpMeasures.Generators.Scalars;

using OneOf;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;

public interface IScalar : IQuantity
{
    public abstract bool UseUnitBias { get; }

    public abstract OneOf<IRawVectorType, IRawVectorGroupType, IRawVectorGroupMemberType>? Vector { get; }

    new public abstract IRawScalarType Difference { get; }

    public abstract IRawScalarType? Reciprocal { get; }
    public abstract IRawScalarType? Square { get; }
    public abstract IRawScalarType? Cube { get; }
    public abstract IRawScalarType? SquareRoot { get; }
    public abstract IRawScalarType? CubeRoot { get; }
}
