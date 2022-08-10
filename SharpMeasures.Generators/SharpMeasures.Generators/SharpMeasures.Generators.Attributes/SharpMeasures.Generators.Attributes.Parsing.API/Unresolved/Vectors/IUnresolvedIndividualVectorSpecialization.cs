namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedIndividualVectorSpecialization : IUnresolvedIndividualVector, IUnresolvedVectorGroupSpecialization
{
    public abstract NamedType OriginalVector { get; }
}
