namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorGroupSpecialization : IRawVectorGroup, IRawQuantitySpecialization
{
    public abstract NamedType OriginalVectorGroup { get; }
}
