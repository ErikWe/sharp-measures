namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarSpecialization : IScalar, IQuantitySpecialization
{
    public abstract NamedType OriginalScalar { get; }
    
    public abstract bool InheritBases { get; }
}
