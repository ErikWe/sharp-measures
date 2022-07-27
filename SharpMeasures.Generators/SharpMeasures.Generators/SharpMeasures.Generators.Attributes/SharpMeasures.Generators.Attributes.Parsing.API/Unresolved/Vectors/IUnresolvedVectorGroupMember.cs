namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedVectorGroupMember : ISharpMeasuresObject
{
    public abstract NamedType VectorGroup { get; }

    public abstract int Dimension { get; }
}
