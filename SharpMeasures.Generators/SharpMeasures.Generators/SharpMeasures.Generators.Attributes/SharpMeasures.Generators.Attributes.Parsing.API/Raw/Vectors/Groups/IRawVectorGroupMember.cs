namespace SharpMeasures.Generators.Raw.Vectors.Groups;

public interface IRawVectorGroupMember : ISharpMeasuresObject
{
    public abstract NamedType VectorGroup { get; }

    public abstract int Dimension { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }
}
