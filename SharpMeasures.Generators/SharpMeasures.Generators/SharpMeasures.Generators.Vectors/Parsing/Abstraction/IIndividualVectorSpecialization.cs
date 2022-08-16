namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Raw.Vectors;

internal interface IIndividualVectorSpecialization : IVector
{
    public abstract IRawVectorType OriginalIndividualVector { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }
}
