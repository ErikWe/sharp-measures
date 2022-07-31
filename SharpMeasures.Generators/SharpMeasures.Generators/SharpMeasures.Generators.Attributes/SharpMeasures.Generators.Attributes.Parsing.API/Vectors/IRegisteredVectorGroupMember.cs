namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Vectors;

public interface IRegisteredVectorGroupMember
{
    public abstract IUnresolvedVectorGroupMemberType Vector { get; }

    public abstract int Dimension { get; }
}
