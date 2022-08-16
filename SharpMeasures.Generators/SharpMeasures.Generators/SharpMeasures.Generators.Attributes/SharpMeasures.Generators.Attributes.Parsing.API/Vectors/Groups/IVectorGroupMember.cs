namespace SharpMeasures.Generators.Vectors.Groups;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Vectors.Groups;

public interface IVectorGroupMember : IQuantity
{
    public abstract int Dimension { get; }

    public abstract IRawVectorGroupType VectorGroup { get; }
}
