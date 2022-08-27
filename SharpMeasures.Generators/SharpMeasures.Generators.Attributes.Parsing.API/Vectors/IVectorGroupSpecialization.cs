namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupSpecialization : IVectorGroup, IQuantitySpecialization
{
    public abstract NamedType OriginalVectorGroup { get; }
}
