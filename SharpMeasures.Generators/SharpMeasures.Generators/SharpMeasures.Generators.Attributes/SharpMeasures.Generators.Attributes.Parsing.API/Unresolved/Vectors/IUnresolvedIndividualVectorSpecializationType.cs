namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedIndividualVectorSpecializationType : IUnresolvedIndividualVectorType, IUnresolvedVectorGroupSpecializationType
{
    new public abstract IUnresolvedIndividualVectorSpecialization Definition { get; }
}
