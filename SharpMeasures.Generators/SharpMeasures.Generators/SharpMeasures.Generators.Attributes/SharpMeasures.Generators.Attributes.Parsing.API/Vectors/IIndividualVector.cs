namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Vectors;

public interface IIndividualVector : IVectorGroup
{
    public abstract int Dimension { get; }
}
