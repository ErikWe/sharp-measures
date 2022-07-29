namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedIndividualVectorSpecialization : IUnresolvedIndividualVector, IUnresolvedVectorGroupSpecialization
{
    public abstract NamedType OriginalVector { get; }
}
