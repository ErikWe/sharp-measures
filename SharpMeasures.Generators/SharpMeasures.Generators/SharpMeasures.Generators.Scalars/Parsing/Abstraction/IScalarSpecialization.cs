namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Generators.Raw.Scalars;

internal interface IScalarSpecialization : IScalar
{
    public abstract IRawScalarType OriginalScalar { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritBases { get; }
    public abstract bool InheritUnits { get; }
}
