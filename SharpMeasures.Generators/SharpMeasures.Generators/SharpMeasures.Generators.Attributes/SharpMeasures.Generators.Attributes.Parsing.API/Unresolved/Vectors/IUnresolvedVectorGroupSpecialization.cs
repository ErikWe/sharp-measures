namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedVectorGroupSpecialization : IUnresolvedVectorGroup, IUnresolvedQuantitySpecialization
{
    public abstract NamedType OriginalVectorGroup { get; }
}
