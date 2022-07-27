namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Vectors;

public interface IVectorGroupMember : IIndividualVector
{
    public abstract IUnresolvedVectorGroupType VectorGroup { get; }
}
