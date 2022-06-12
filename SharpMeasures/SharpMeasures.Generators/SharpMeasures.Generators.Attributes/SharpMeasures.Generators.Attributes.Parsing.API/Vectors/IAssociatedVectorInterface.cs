namespace SharpMeasures.Generators.Vectors;

public interface IAssociatedVectorInterface : IVectorInterface
{
    public abstract NamedType AssociatedVector { get; }
}
