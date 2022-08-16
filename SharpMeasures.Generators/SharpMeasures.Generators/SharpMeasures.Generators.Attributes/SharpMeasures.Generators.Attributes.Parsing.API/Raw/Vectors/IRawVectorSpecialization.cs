namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorSpecialization : IRawVector, IRawQuantitySpecialization
{
    public abstract NamedType OriginalVector { get; }
}
