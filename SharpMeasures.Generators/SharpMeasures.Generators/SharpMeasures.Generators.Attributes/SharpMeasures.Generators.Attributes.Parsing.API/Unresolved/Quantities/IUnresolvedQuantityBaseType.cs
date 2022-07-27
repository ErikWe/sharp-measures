namespace SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedQuantityBaseType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedQuantityBase Definition { get; }
}
