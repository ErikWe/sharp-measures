namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Unresolved.Vectors;

internal interface IVectorGroupSpecialization : IVectorGroup
{
    public abstract IUnresolvedVectorGroupType OriginalVectorGroup { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }
}
