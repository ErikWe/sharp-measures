namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorGroupSpecializationType : IRawVectorGroupType, IRawQuantitySpecializationType
{
    new public abstract IRawVectorGroupSpecialization Definition { get; }
}
