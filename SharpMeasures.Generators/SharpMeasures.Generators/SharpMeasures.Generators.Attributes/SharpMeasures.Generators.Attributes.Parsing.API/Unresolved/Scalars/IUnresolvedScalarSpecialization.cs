namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedScalarSpecialization : IUnresolvedScalar, IUnresolvedQuantitySpecialization
{
    public abstract NamedType OriginalScalar { get; }
    
    public abstract bool InheritConstants { get; }
    public abstract bool InheritBases { get; }
}
