namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorGroup : IRawQuantity
{
    public abstract NamedType? Scalar { get; }
}
