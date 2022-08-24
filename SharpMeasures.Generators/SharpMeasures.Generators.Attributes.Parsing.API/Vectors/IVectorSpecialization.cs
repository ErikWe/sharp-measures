namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorSpecialization : IVector, IQuantitySpecialization
{
    public abstract NamedType OriginalVector { get; }
}
