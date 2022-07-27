namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedVectorGroupSpecializationType : IUnresolvedVectorGroupType, IUnresolvedQuantitySpecializationType
{
    new public abstract IUnresolvedVectorGroupSpecialization Definition { get; }
}
