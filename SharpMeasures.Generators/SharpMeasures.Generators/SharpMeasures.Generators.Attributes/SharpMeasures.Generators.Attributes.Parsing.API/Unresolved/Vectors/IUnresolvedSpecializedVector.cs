namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedSpecializedVector : IUnresolvedVector, IUnresolvedSpecializedQuantity
{
    public abstract NamedType OriginalVector { get; }

    public abstract bool InheritConvertibleVectors { get; }
}
