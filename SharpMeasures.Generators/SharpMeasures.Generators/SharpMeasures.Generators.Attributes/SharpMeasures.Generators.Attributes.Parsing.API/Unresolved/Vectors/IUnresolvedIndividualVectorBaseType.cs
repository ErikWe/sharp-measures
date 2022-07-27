namespace SharpMeasures.Generators.Unresolved.Vectors;

public interface IUnresolvedIndividualVectorBaseType : IUnresolvedIndividualVectorType, IUnresolvedVectorGroupBaseType
{
    new public abstract IUnresolvedIndividualVectorBase Definition { get; }
}
