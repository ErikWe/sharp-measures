namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Vectors;

public interface IVectorGroupMember : IVector
{
    public abstract IUnresolvedVectorGroup Group { get; }
}
