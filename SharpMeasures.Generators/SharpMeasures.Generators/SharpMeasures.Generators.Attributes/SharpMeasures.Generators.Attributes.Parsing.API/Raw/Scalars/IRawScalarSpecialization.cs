namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawScalarSpecialization : IRawScalar, IRawQuantitySpecialization
{
    public abstract NamedType OriginalScalar { get; }
    
    public abstract bool InheritBases { get; }
}
