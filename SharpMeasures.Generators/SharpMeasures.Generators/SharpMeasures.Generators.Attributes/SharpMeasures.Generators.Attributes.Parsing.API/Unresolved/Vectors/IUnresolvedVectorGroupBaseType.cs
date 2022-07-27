namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedVectorGroupBaseType : IUnresolvedVectorGroupType, IUnresolvedQuantityBaseType
{
    new public abstract IUnresolvedVectorGroupBase Definition { get; }
}
