namespace SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedDerivedQuantity
{
    public abstract string Expression { get; }
    public abstract UnresolvedQuantityDerivationSignature Signature { get; }
}
