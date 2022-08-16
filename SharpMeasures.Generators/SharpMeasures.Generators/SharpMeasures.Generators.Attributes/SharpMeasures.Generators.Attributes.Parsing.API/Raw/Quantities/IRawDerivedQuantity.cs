namespace SharpMeasures.Generators.Raw.Quantities;

public interface IRawDerivedQuantity
{
    public abstract string Expression { get; }
    public abstract RawQuantityDerivationSignature Signature { get; }
}
