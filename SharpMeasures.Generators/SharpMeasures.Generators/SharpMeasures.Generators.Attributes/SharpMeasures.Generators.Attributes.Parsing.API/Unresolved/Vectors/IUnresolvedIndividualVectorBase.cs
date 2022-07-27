namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedIndividualVectorBase : IUnresolvedIndividualVector, IUnresolvedVectorGroupBase
{
    public abstract int Dimension { get; }
}
