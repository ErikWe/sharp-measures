namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Vectors;

public interface ISpecializedVectorGroup : IVectorGroup
{
    public abstract IUnresolvedVectorGroupType OriginalVectorGroup { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConvertibleVectors { get; }
    public abstract bool InheritUnits { get; }
}
