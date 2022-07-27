namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Unresolved.Vectors;

internal interface IIndividualVectorSpecialization : IIndividualVector
{
    public abstract IUnresolvedIndividualVectorType OriginalIndividualVector { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }
}
