namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Vectors;

public interface IRegisteredVectorGroupMember
{
    public abstract IUnresolvedIndividualVectorType Vector { get; }

    public abstract int Dimension { get; }
}
