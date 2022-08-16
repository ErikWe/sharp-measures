namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorGroupBaseType : IRawVectorGroupType, IRawQuantityBaseType
{
    new public abstract IRawVectorGroupBase Definition { get; }
}
