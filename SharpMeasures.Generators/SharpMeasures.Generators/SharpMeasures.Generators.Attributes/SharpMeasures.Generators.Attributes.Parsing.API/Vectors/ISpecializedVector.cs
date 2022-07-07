namespace SharpMeasures.Generators.Vectors;

public interface ISpecializedVector : IVector
{
    public abstract NamedType OriginalVector { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritUnits { get; }
}
