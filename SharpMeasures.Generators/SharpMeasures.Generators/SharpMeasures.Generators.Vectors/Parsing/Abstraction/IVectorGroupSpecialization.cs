namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Groups;

internal interface IVectorGroupSpecialization : IVectorGroup
{
    public abstract IRawVectorGroupType OriginalVectorGroup { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }
}
